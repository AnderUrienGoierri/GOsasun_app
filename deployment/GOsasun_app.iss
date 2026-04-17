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

[Files]
Source: "{#MyPublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\sql\*"; DestDir: "{app}\sql"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Abiarazi {#MyAppName}"; Flags: nowait postinstall skipifsilent

[Code]
var
  WebErroaOrria: TInputDirWizardPage;
  DatuBaseOrria: TInputQueryWizardPage;
  HasierakoPrestaketaOrria: TInputOptionWizardPage;

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

procedure InitializeWizard();
begin
  WebErroaOrria := CreateInputDirPage(
    wpSelectDir,
    'Biltegiratze bideak',
    'Konfiguratu Apache/web erroa',
    'Hemen gorde dira dokumentuak, XML fitxategiak eta pazienteen irudiak. UNC bide bat ere erabil dezakezu, adibidez \\192.168.1.20\GOsasun_web.',
    False,
    '');
  WebErroaOrria.Add('');
  WebErroaOrria.Values[0] := 'C:\Apache24-64\htdocs\GOsasun_web';

  DatuBaseOrria := CreateInputQueryPage(
    WebErroaOrria.ID,
    'Datu-basearen konfigurazioa',
    'Konfiguratu SQL zerbitzaria',
    'Jarri SQL zerbitzariaren IP/hostname-a eta GOsasun aplikazioak erabili behar duen datu-basea.');
  DatuBaseOrria.Add('Zerbitzaria / IP-a:', False);
  DatuBaseOrria.Add('Portua:', False);
  DatuBaseOrria.Add('Datu-basearen izena:', False);
  DatuBaseOrria.Add('Erabiltzailea:', False);
  DatuBaseOrria.Add('Pasahitza:', True);
  DatuBaseOrria.Values[0] := 'localhost';
  DatuBaseOrria.Values[1] := '3306';
  DatuBaseOrria.Values[2] := 'GOsasun_DB';
  DatuBaseOrria.Values[3] := 'root';
  DatuBaseOrria.Values[4] := '1MG32025';

  HasierakoPrestaketaOrria := CreateInputOptionPage(
    DatuBaseOrria.ID,
    'Lehen exekuzioko prestaketa',
    'Aukeratu zer prestatu behar den lehen aldiz abiaraztean',
    'Instalazioak balioak gordeko ditu, eta aplikazioak lehen exekuzioan automatikoki exekutatuko ditu aukeratutako SQL scriptak.',
    False,
    False);
  HasierakoPrestaketaOrria.Add('Datu-base eskema sortu edo eguneratu lehen exekuzioan');
  HasierakoPrestaketaOrria.Add('Lehen erregistroak eta seed datuak kargatu lehen exekuzioan');
  HasierakoPrestaketaOrria.Values[0] := True;
  HasierakoPrestaketaOrria.Values[1] := True;
end;

function JsnEscape(const Value: string): string;
var
  Escaped: string;
begin
  Escaped := Value;
  StringChangeEx(Escaped, '\', '\\', True);
  StringChangeEx(Escaped, '"', '\"', True);
  StringChangeEx(Escaped, #13#10, '\n', True);
  StringChangeEx(Escaped, #10, '\n', True);
  StringChangeEx(Escaped, #13, '\n', True);
  Result := Escaped;
end;

function BoolToJson(const Value: Boolean): string;
begin
  if Value then
  begin
    Result := 'true';
  end
  else
  begin
    Result := 'false';
  end;
end;

function SortuAppSettingsJson(): string;
begin
  Result :=
    '{' + #13#10 +
    '  "bertsioa": 1,' + #13#10 +
    '  "datuBasea": {' + #13#10 +
    '    "zerbitzaria": "' + JsnEscape(DatuBaseOrria.Values[0]) + '",' + #13#10 +
    '    "portua": ' + JsnEscape(DatuBaseOrria.Values[1]) + ',' + #13#10 +
    '    "datuBasea": "' + JsnEscape(DatuBaseOrria.Values[2]) + '",' + #13#10 +
    '    "erabiltzailea": "' + JsnEscape(DatuBaseOrria.Values[3]) + '",' + #13#10 +
    '    "pasahitza": "' + JsnEscape(DatuBaseOrria.Values[4]) + '"' + #13#10 +
    '  },' + #13#10 +
    '  "biltegiratzea": {' + #13#10 +
    '    "webErroa": "' + JsnEscape(WebErroaOrria.Values[0]) + '"' + #13#10 +
    '  },' + #13#10 +
    '  "abioa": {' + #13#10 +
    '    "lehenAbioEgiaztatua": false,' + #13#10 +
    '    "sortuDatuBaseEskemaLehenAbioan": ' + BoolToJson(HasierakoPrestaketaOrria.Values[0]) + ',' + #13#10 +
    '    "kargatuHasierakoDatuakLehenAbioan": ' + BoolToJson(HasierakoPrestaketaOrria.Values[1]) + #13#10 +
    '  }' + #13#10 +
    '}' + #13#10;
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;

  if CurPageID = DatuBaseOrria.ID then
  begin
    if Trim(DatuBaseOrria.Values[0]) = '' then
    begin
      MsgBox('SQL zerbitzariaren IP edo hostname-a bete behar duzu.', mbError, MB_OK);
      Result := False;
      exit;
    end;

    if Trim(DatuBaseOrria.Values[1]) = '' then
    begin
      MsgBox('SQL portua bete behar duzu.', mbError, MB_OK);
      Result := False;
      exit;
    end;

    if Trim(DatuBaseOrria.Values[2]) = '' then
    begin
      MsgBox('Datu-basearen izena bete behar duzu.', mbError, MB_OK);
      Result := False;
      exit;
    end;
  end;

  if CurPageID = HasierakoPrestaketaOrria.ID then
  begin
    if HasierakoPrestaketaOrria.Values[1] and (not HasierakoPrestaketaOrria.Values[0]) then
    begin
      MsgBox('Hasierako datuak kargatzeko, lehenik eskema sortzea aktibatu behar da.', mbError, MB_OK);
      Result := False;
      exit;
    end;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    ForceDirectories(WebErroaOrria.Values[0]);
    ForceDirectories(AddBackslash(WebErroaOrria.Values[0]) + 'dokumentuak');
    ForceDirectories(AddBackslash(WebErroaOrria.Values[0]) + 'paziente_dokumentuak');
    ForceDirectories(AddBackslash(WebErroaOrria.Values[0]) + 'xml_paziente_neurketak');
    ForceDirectories(AddBackslash(WebErroaOrria.Values[0]) + 'img\png');

    SaveStringToFile(ExpandConstant('{app}\appsettings.json'), SortuAppSettingsJson(), False);
  end;
end;