#define MyAppName "Auto Pecas Concorrente ERP"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Auto Pecas Concorrente"
#define MyAppExeName "AutoPecasConcorrenteERP.exe"
[Setup]
AppId={{B70863E4-A41B-47C8-97E6-AC0D87B33715}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\Auto Pecas Concorrente ERP
DefaultGroupName={#MyAppName}
OutputDir=Output
OutputBaseFilename=AUTO PECAS CONCORRENTE ERP SETUP
Compression=lzma2
SolidCompression=yes
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
PrivilegesRequired=admin
WizardStyle=modern
UninstallDisplayIcon={app}\{#MyAppExeName}
[Files]
Source: "..\publish\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Abrir {#MyAppName}"; Flags: nowait postinstall skipifsilent
