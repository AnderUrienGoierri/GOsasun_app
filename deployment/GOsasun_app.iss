#define MyAppName "GOsasun App"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "GOsasun"
#define MyAppExeName "GOsasun_app.exe"
#define MyPublishDir "..\\publish\\win-x64"

[Setup]
AppId={{2A5E7560-5E20-4D76-A6A0-7B6D8A9476E4}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName=C:\GOsasun_app
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=output
OutputBaseFilename=GOsasun_app_Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "basque"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Sortu mahaigaineko lasterbidea"; GroupDescription: "Lasterbideak:"; Flags: unchecked

[Dirs]
Name: "C:\Apache24-64\htdocs\GOsasun_web\dokumentuak"
Name: "C:\Apache24-64\htdocs\GOsasun_web\paziente_dokumentuak"
Name: "C:\Apache24-64\htdocs\GOsasun_web\xml_paziente_neurketak"
Name: "C:\Apache24-64\htdocs\GOsasun_web\img\png"

[Files]
Source: "{#MyPublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\sql\*"; DestDir: "{app}\sql"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Abiarazi {#MyAppName}"; Flags: nowait postinstall skipifsilent

[Code]
function InitializeSetup(): Boolean;
begin
  Result := True;
  if not DirExists(ExpandConstant('{src}\..\publish\win-x64')) then
  begin
    MsgBox(
      'Ez da publish karpeta aurkitu.' + #13#10 +
      'Lehenik exekutatu deployment\\Publish-GOsasun.ps1 script-a.',
      mbError,
      MB_OK);
    Result := False;
  end;
end;