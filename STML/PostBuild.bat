@echo "Start Postbuild script:"
@echo "Start copying"
echo %1
@echo "to"
echo %2
xcopy /y %1 %2