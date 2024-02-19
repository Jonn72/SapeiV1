@echo off
start "C:\Users\Manuel\AppData\Local\Programs\MiKTeX\miktex\bin\x64" pdflatex.exe --shell-escape C:\Users\Manuel\Source\Repos\Sapei\appSapei\texample.tex
:loop
tasklist /fi "imagename eq pdflatex.exe" |find ":" > nul
if errorlevel 1 goto loop
exit 