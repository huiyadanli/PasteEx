"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe" ..\PasteEx.sln /t:Rebuild /p:Configuration=Release

echo 打包中...
xcopy /y "..\PasteEx\bin\Release\PasteEx.exe" PasteEx\
xcopy /s /y "..\PasteEx\bin\Release\User" PasteEx\User\
xcopy /s /y "..\PasteEx\bin\Release\Language" PasteEx\Language\
"D:/Program Files/WinRAR/WinRAR.exe" a -as -r "PasteEx.v%~1.zip" "PasteEx"

echo 打包完成，删除 PasteEx 目录

rd /s /q PasteEx
pause