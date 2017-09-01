@echo off
PUSHD " "
xcopy "KML Converter.exe" "C:\Users\Public"
C:
CD "C:\Users\Public"
"KML Converter.exe"
DEL "KML Converter.exe"
POPD