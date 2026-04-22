# Git Bash Erabilera Gida

Gida honetan, Git Bash erabiliz zure proiektuko karpetara mugitzeko, adarkatze (branch) batetik bestera pasatzeko, aldaketak egiteko, komiteatzeko eta batzeko (merge) eman behar dituzun pausoak azaltzen dira.

## 1. Proiektua ireki (Direktoriora mugitu)

Git Bash ireki ondoren, lehenengo pausoa zure proiektuaren karpetara mugitzea da. Windows-eko ibilbideak Git Bash-en formatu berezian idazten dira (adibidez, `C:\` beharrean `/c/` erabiliz).

```bash
cd /c/Ander/Workspace/C/proiektuak/GOsasun_app
```

* `cd` (Change Directory) komandoak adierazitako karpetara mugitzeko balio du.

## 2. `main` adarretik `ander-branch` adarrera mugitu

Zure lana `ander-branch` adarrean (branch) egiteko, adar horretara aldatu behar zara. Adar hori jada sortuta badago:

```bash
git checkout ander-branch
```
*(Edo `git switch ander-branch` bertsio berriagoetan).*

Oharra: `ander-branch` adarra oraindik sortu gabe badago, komando hau erabili behar duzu sortu eta aldi berean bertara mugitzeko:
```bash
git checkout -b ander-branch
```

## 3. Aldaketa bat egin (.txt artxiboa sortu)

Orain, `ander-branch` adarrean gaudela, fitxategi berri bat sortuko dugu `gitbash_prueba` testuarekin `C:\Ander\Workspace\C\proiektuak\GOsasun_app` karpetan.

```bash
echo "gitbash_prueba" > proba.txt
```

* Komando honek `proba.txt` izeneko fitxategia sortzen du zuzenean uneko direktorioan, eta barruan "gitbash_prueba" testua idazten du. 

## 4. Aldaketak prestatu eta Commit bat egin (Nano erabiliz)

Fitxategia sortu edo aldatu ondoren, Git-i esan behar diogu aldaketa hori gorde nahi dugula.

Lehenik, aldaketa "staging area"-ra (prestatze-eremura) gehitu behar dugu:

```bash
git add proba.txt
```

Ondoren, `commit` bat egingo dugu. Commit-aren mezua idazteko `nano` editorea irekitzea nahi badugu (askotan Vim izaten da lehenetsia Git Bash-en), komando hau erabiliko dugu:

```bash
GIT_EDITOR=nano git commit
```
*(Edo zuzenean `git commit` baldin eta Nano badaukazu lehenetsitako editore gisa ezarrita).*

**Nano barruan:**
1. Idatzi commit-aren deskribapena pantailaren goialdean, adibidez: *proba.txt fitxategia sortu da*
2. Gorde aldaketak: `Ctrl + O` sakatu, eta ondoren gorde nahi duzun fitxategi-izena baieztatzeko `Enter` sakatu.
3. Nanotik irten: `Ctrl + X` sakatu.

## 5. `ander-branch`-eko aldaketak `main`-era merge egin

`ander-branch`-ean aldaketa (commit-a) gorde dugunean, adar nagusiarekin (`main`) elkartu (merge) behar ditugu egin ditugun aldaketak.

Lehenik, `main` adarrera itzuli behar dugu:

```bash
git checkout main
```

Ondoren, `ander-branch`-en egindako aldaketak `main`-era batu egingo ditugu:

```bash
git merge ander-branch
```

Horrela, `proba.txt` fitxategia (eta egin dugun commit-a) adar nagusian egongo dira eskuragarri.

---

## 🚀 Oinarrizko Git Komandoen Zerrenda (Git.pdf)

Klasean ikusitako `Git.pdf` teoriako oinarrizko komando erabilgarrien zerrenda bat:

* **Egoera ikusteko:**
  * `git status`: Fitxategien uneko egoera erakusten du (aldatuta dauden, gehitu gabe, prestatuta, etab.).
* **Adarrak (Branches):**
  * `git branch`: Dauden adar guztiak zerrendatzen ditu.
  * `git branch <adar-izena>`: Adar berri bat sortzen du.
* **Aldaketak ikusteko:**
  * `git log`: Orain arte egindako commit-en historia eta deskribapenak erakusten ditu.
  * `git diff`: Gordeta ez dauden fitxategien arteko ezberdintasunak erakusten ditu.
* **Errepositorioekin lan egiteko:**
  * `git init`: Direktorio batean Git errepositorio berri bat hasieratzen du.
  * `git clone <url>`: Proiektu baten kopia bat deskargatzen du sareko errepositoriotik (adib. GitHub).
  * `git pull`: Urrutiko adarreko aldaketak ekarri eta batzen ditu uneko tokiko adarrarekin.
  * `git push`: Zure tokiko commit-ak urrutiko errepositoriora (zerbitzarira) igotzen ditu.
