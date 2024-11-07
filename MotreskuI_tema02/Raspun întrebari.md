# Raspun întrebari

a. **Viewport** este secțiunea ferestrei utilizatorului în care se desenează scena din OpenGL. Această metodă mapează pur și simplu scena pe o porțiune a ferestrei.

b. **FPS** (cadre pe secundă) reprezintă numărul de imagini afișate pe ecran într-o secundă. Este rata la care OpenGL redă cadrele pentru a crea o scenă dinamică.

c. Metoda **OnUpdateFrame()** rulează în cadrul ciclului principal de randare și este folosită pentru a calcula mișcarea obiectelor, coliziunile acestora și pentru a prelucra datele de intrare de la utilizator.

d. **Modul de randare imediată** este o tehnică prin care geometria și parametrii obiectelor grafice sunt trimiși direct în pipeline-ul grafic la fiecare cadru, pe măsură ce acesta este redat.

e. Ultima versiune de **OpenGL** care acceptă modul de randare imediată este **3.0**.

f. Metoda **OnRenderFrame()** este apelată în ciclul de randare și afișează grafica actualizată în metoda **OnUpdateFrame()**, fiind apelată după aceasta.

g. Metoda **OnResize()** trebuie executată cel puțin o dată pentru a seta dimensiunea Viewport-ului, care definește zona în care este desenată scena. De asemenea, aceasta ajută la menținerea proporțiilor corecte ale scenei când se modifică dimensiunea ferestrei.

h. Parametrii metodei **CreatePerspectiveFieldOfView()** sunt:

- **FOV**: unghiul de deschidere al câmpului vizual pe verticală (între 0.1 și π).
- **Aspect Ratio**: raportul dintre lățimea și înălțimea ferestrei (trebuie să fie >0).
- **Near Plane**: distanța minimă de la cameră la planul apropiat (trebuie să fie >0).
- **Far Plane**: distanța maximă până la planul îndepărtat (trebuie să fie >0).