# MotreskuI_tema03

1. a)
    
    Antiorar. Comentand desenarea axei X, dispare linia din dreapta. Comentand desenarea axei y dispare linia de sus
    
    b)
    

```csharp
private void DrawAxes() {

GL.Begin(PrimitiveType.Lines);

GL.Color3(Color.Red);
GL.Vertex3(0, 0, 0);
GL.Vertex3(XYZ_SIZE, 0, 0);

GL.Color3(Color.Yellow);
GL.Vertex3(0, 0, 0);
GL.Vertex3(0, XYZ_SIZE, 0);

GL.Color3(Color.Green);
GL.Vertex3(0, 0, 0);
GL.Vertex3(0, 0, XYZ_SIZE);

GL.End();
```

1. Anti-aliasing-ul funcționează prin aplicarea unor pixeli gri pe marginile unei linii sau curbe pentru a le amesteca cu fundalul. Acest proces reduce efectul de "dinte de fierăstrău", oferind un aspect mai neted și mai puțin vizibil al marginilor.
2. a) Se modifica grosimea liniilor desenate
    
    b) Se modifica grosimea punctelor desenate
    
    c) Nu. Metoda `GL.Begin()` aplică setările tuturor entităților declarate înainte de apelarea sa.
    
3. a) `LineLoop` creează o buclă de linii care leagă toate punctele succesive, conectând ultimul punct la primul.
    
    b) `LineStrip` este similar cu `LineLoop`, dar nu leagă primul punct cu ultimul.
    
    c) `TriangleFan` folosește primul punct ca centru, iar toate celelalte puncte formează triunghiuri legate de acest punct central. Fiecare punct nou adăugat este conectat de asemenea la punctul central și cel precedent.
    
    d) `TriangleStrip` conectează punctele specificate (minimum 3) pentru a forma o bandă continuă de triunghiuri, unde fiecare punct nou formează un triunghi împreună cu ultimele două puncte anterioare.
    
4. Permite o diferențiere mai ușoară a mai multor obiecte
5. Simulează efecte de lumină și umbră
6. a) Un gradient este o tranziție lină între două sau mai multe culori
    
    b) Exemplu: Se poate crea un triunghi, oferindu-i fiecărei vârfuri o culoare diferită pentru a produce un efect de gradient
    

Rezolvare:

```csharp
private void DrawAxes() {

    // Triunghi cu gradient
    GL.Begin(PrimitiveType.Triangles);

    GL.Color3(Color.AliceBlue);
    GL.Vertex2(-2, -2);

    GL.Color3(Color.Red);
    GL.Vertex2(2, -2);

    GL.Color3(Color.Green);
    GL.Vertex2(2, 2);

    GL.End();
}
```

Ex 10

Se aplica gradientul, ca si la ex 7.b
