# Хронение конфигураций из объекта

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
  
    public List<string> SetingList{get;set;}
}
```
Создается глобальная переменная, в которой указывается путь к файлу
```C#
using myConfig;

myConfig.Config<Setting> set = new myConfig.Config<Setting>("config.xml"); //если файла не было, то создается в соответствии со структурой указанного класса и вставляются значения по умолчанию
void main()
{
    Console.Write(set.config.Seting1); //чтение данных
    set.config.Seting1 = "valval2"; //запись в класс
    set.writeConfig(set.config); //сохранение в файл
}
```
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
