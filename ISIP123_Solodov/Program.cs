
string _adds = "Привет Магнитола (+12 999₽) GDFKPSDo";
Console.WriteLine(_adds);
int start = _adds.IndexOf("Привет Магнитола (+12 999₽)");
int end = start + "Привет Магнитола (+12 999₽)".Length;
_adds = _adds.Substring(0, start);
Console.WriteLine(_adds + 2134);
_adds = _adds.Substring(end);

Console.WriteLine(_adds + 1234123);
