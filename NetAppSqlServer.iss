[Setup]
AppName=NetAppSqlServer
AppVersion=${VERSION}
DefaultDirName={autopf}\NetAppSqlServer
DefaultGroupName=NetAppSqlServer
UninstallDisplayIcon={app}\NetAppSqlServer.exe
Compression=lzma2
SolidCompression=yes
OutputDir=installer
OutputBaseFilename=NetAppSqlServerInstaller
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64

[Files]
Source: "publish\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion

[Icons]
Name: "{group}\NetAppSqlServer"; Filename: "{app}\NetAppSqlServer.exe"
