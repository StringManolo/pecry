# pecry
PE Crypter

### usage
```
  Usage: mono crypter.exe --file malware.exe

-f --file     File to crypt
-k --key      Key to use
-o --output   Runnable encrypted malware
-s --silence  Do not output warning messages to stdout
-h --help     Show this help message

  Example:
mono crypter.exe --file malware.exe --key qwerty --silence > cryptedMalware.cs
csc cryptedMalware.cs
```
