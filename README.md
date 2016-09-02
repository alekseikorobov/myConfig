# Хранение конфигураций из объекта

## Использование

1. Создается класс где будут храниться все значения
Например:
```C#
public class Setting{
    public Setting(){
        //значения по умолчанию
        Seting1 = "val1"; 
        Seting2 = "val2";
    }
    public string Seting1{get;set;}
    public string Seting2{get;set;}
    public string Seting3{get;set;}
}
```
2. Создается глобальная переменная, в которой указывается путь к файлу
```C#
using myConfig;

myConfig.Config<Setting> set = new myConfig.Config<Setting>("config.xml"); 
//если файла не было, то создается в соответствии со структурой 
//указанного класса и вставляются значения по умолчанию
void main()
{
    Console.Write(set.config.Seting1); //чтение данных
    set.config.Seting1 = "valval2"; //запись в класс
    set.writeConfig(set.config); //сохранение в файл
}
```
результат в виде xml, который удобно читать и изменять
```xml
<?xml version="1.0" encoding="utf-8"?>
<Setting xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Seting1>valval2</Seting1>
  <Seting2>val2</Seting2>
</Setting>
```
технология основана на сереализации объекта, соответственно типы данных в классе могут быть любые, которые могут быть сереализованы, к ним относятся все стандартные типы данных, так же коллекции и даже пользовательские классы.
При полученини данных явное преобразование к нужно типу не требуется.
## Дополнительное шифрование данных в файле
Для шифрования данных имеется класс Crypt, которые имеет два статических метода Encrypt и Decrypt (шифорвание и деширование соответственно)
Использование:
```C#
void main()
{
    string secretPass = "1234"; //ключ к шифрованию, которые не должен быть непосредственно в программе
    Console.Write(myConfig.Crypt.Decrypt(set.config.Seting1,secretPass)); //чтение зашифрованных данных
    set.config.Seting1 = "valval2"; //запись в класс
    set.writeConfig(myConfig.Crypt.Encrypt(set.config,secretPass)); //сохранение в файл в шифрованном виде
}
```
## Ограничения
1. в классе конфигураций всегда должен быть явно указан конструктор без параметров
2. при указании пути нельзя использовать папки, которые не существуют
