# Animetra STML Library (Streentext Markup Language)
This is a C# library to save, load and return screen text, e.g. for games made with Unity.
It's meant to be used for translation and implementing on-screen text conversations.

## Concept

Texts are saved in XML-documents with a proprietary structure (STML).
This library gives the necessary functions to read and write these files and to return the needed content via IDs either as unformatted string or with RTF style tags.

The most important features are:

- Saving and reading texts in STML.
- Rich Text Format functionality, [have a look here](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/StyledText.html)
- Light-weight structuring tools
- Satisfying many use cases like subtitles, translation, on-screen dialogues and so on

## Quick Start

Do this to get the formatted text of an expression:

1. Write your text into a .stml-file with the needed structure.
1. Create a `new STMLReader()` and use `STMLReader.ReadFile()` to load your Project.xml file. It returns a `STMLProject`.
1. Use `STMLProject.SetActiveLanguage()` to set the desired language.
1. Use `STMLProject.GetDocument()` to get the desired document via its id.
1. Use `STMLDocument.Resolve()` to resolve possible references.
1. Use `STMLDocument.GetSection()` to get the desired section via its id.
1. Use `STMLSection.GetExpression()` on that section to get the expression you want via its id or its index.
1. Use `STMLExpression.GetFormattedText()` on that expression to get the expression's formatted text or `STMLExpression.GetPlainText()` to get the unformatted text.

## Folder structure of a STML Project

Example:

[Unity Project]
└── Assets
	└── STML
		├── Project.xml
		├── en
		│	├── Basic.xml
		│	├── World 1.xml
		│	└── World 2.xml
		└── de
			├── Basic.xml
			├── World 1.xml
			└── World 2.xml

## Model

| Term | represented by C# class | does | Example |
| --- | --- | --- | --- |
| Reader | `STMLReader` | Reads a .stml-file and returns an `STMLDocument`. | - |
| Writer | `STMLWriter` | Writes a `STMLDocument` to an .xml-file. | - |
| Project | `STMLProject` | Carries the whole screentext project. The project.xml file contains basic information about the project. | "Stone Skipper Dash" |
| Element | `STMLElement` | An abstract base class for implementing hierarchy functionality and an header.| - |
| Header | `STMLHeader` | The header of an `STMLElement` | - |
| Document | `STMLDocument` | A data structure representing a bigger unit inside the game. | "Basic" / "World 1" |
| Section | `STMLSection` | An abstract base class for structural units inside a `STMLDocument` to organize your texts. Can be an `STMLDictionary` or an `STMLScript`.| see Dictionary / Script |
| Dictionary | `STMLSection` | A structural unit inside a `STMLDocument` to organize single terms or names. | "Menu Terms" / "Character Names" |
| Script | `STMLSection` | A structural unit inside a `STMLDocument` to organize your texts. Contains expressions. | "Conversation A" |
| Term | `STMLTerm` | A single word or term. Can be referenced in other texts. Can't be formatted. | "Start" / "Back" / "Peter" |
| Expression | `STMLExpression` | A formattable phrase, statement or short text | "Good job, you mastered the tutorial!" |
| STMLString | `STMLString` | carries the actual text inside a Term. | - |
| STMLFormattableString | `STMLFormattableString` | carries the actual text inside an expression. | - |

## When to use terms and when expressions

The main difference between terms and expressions is that terms cannot be formatted and expressions cannot be referenced, which is by design.
That said, use expressions for phrases or statements, mainly useful for on-screen dialogues, subtitles or text-effects.
Terms on the other hand are perfect for translating single words and when the style is not content-sensitive, e.g. for the UI like "back" or "Press...". Also use it to make writing expressions easier, since they can by inserted via `<ref>`.

## STML Project Structure

Example:

Stone Skipper Dash [Project]
└── Basic [Document]
	├── Menu Terms [Section: Dictionary]
	│	├── Start [Term]
	│	├── Back [Term]
	│	├── Info [Term]
	│	└── ...
	└── Basic phrases [Section: Script]
		├── If you want to know more about [Expression]
		├── Good job! [Expression]
		├── Welcome to [Expression]
		├── The game is saving, please wait. [Expression]
		└── ...

## STML Document Structure

### Project.xml:
([]-brackets to be replaced by actual values)

	<?xml version="1.0" encoding="utf-8"?>
	<stml version=[]/>
	<root>	
		<name>[]</name>
		<id>[]</id>
		<languages>
			<langcode>[]</langcode>
			<...>
		</languages>
		<mainlanguage>[]</mainlanguage>
	</root>

### Document.xml
([]-brackets to be replaced by actual values)

	<?xml version="1.0" encoding="utf-8"?>
	<stml version>
	<root>	
		<header>
			<name>[]</name>
			<id>[]</id>
			<langcode>[]</langcode>
		</header>

		<content>
			<dictionary name=[] id=[]>
				<term name=[] id=[] refID=[]>[]</term>
				<...>
			</dictionary>
			<script name=[] id=[]>
				<expression name=[] id=[] narrator=[] style=[]>[]</expression>
				<...>
			</script>
			<...>
		</content>
	</root>
	

The language code has to follow [ISO-639-1](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes) in lower case:
English -> en
German -> de
...and so on

## Style options inside expressions

| tag | does | attributes | attribute mandatory / optional | defines |
| --- | --- | --- | --- | --- |
| \<b> | makes text <b>bold</b> | | | |
| \<i> | makes text <i>italic</i> | | | | 
| \<size> | changes <span style="font-size:30px;">text size</span> | value | mandatory | size in pt | 
| \<color> | changes <span style="color:red">text color</span> | value | mandatory | the color in HEX or as keyword | 
| \<material>  | :construction: | value | mandatory | :construction: |
| \<quad> | :construction: | value | mandatory | :construction: |
| \<style> | refers to a style defined in a style sheet | class | mandatory | the class name |
| \<ref>| references and inserts a term (same or other document) | document | mandatory | the id* of the STML document holding the term |
|		|					| section | mandatory | the id* of the section holding the term |
|		|					| term | mandatory | the id of the term |
| \<resource>| inserts a resource (same document) | var | mandatory | the id of the variable |

*You can use "this" to reference the own document or section.

## Referencing

You can insert terms from the same or another document by using the `<ref>`-tag:

`<ref document="otherDoc" section="section A" term="fancyTerm"/>` will be replaced with the content of fancyTerm in section 1 in otherDoc.

:exclamation: To make this work, you have to resolve the respective document. To do this invoke `STMLDocument.Resolve()` at the most suitable moment in your project.