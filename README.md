# paper-cli

Static site generator (SSG) made with [.NET](https://dotnet.microsoft.com/).

Sample website: [https://paper-csharp-sample.vercel.app/](https://paper-csharp.vercel.app/)

## Using as a nuget

Install from nuget: [https://www.nuget.org/account/Packages](https://www.nuget.org/account/Packages)

Usage:

```csharp
using Paper_csharp.Modules.Cli;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator(args);
            generator.Run();
        }
    }
}

```

## Using as a console app

1. Install your version of OS from [Release](https://github.com/Andrewnt219/paper-csharp/releases)
2. Unzip the console app
3. Run the executable file

- Window: .exe
- Linux/Mac: (no extension)

```bash
$ ./Generator.exe --help

  -i, --input         Required. Path to file(s)

  -s, --stylesheet    Path to css file or url

  -o, --output        (Default: ./dist) Path to output directory

  -l, --lang          (Default: en-CA) Locale of generated .html files

  --help              Display this help screen.

  --version           Display version information.
```

## Features

#### 🌟 Generate `index.html`

The index file includes paths to all the generated html files (recursively)

```bash
$ ./Generator.exe -i dir-with-nested-dirs-and-files
```

#### 🌟 Pass in stylesheet's file OR url as a CLI arg

Content of `.css` files are bundled into all the generated `.html` files

```bash
$ ./Generator.exe -i page.txt --stylesheet ./my-style.css
```

#### 🌟 Keep source folder structure

If a directory is passed as `--input`, `dist` keeps the structure of the source dir

```bash
$ ./Generator.exe -i sample-dir

├── sample-dir
├── Cargo.toml
├── sample-dir
│   ├── sub-dir-1
│   └── sub-dir-2
│       └── page-1.txt
├── dist
│   ├─ sample-dir
│       ├── sub-dir-1
│       └── sub-dir-2
│           └── page-1.html
```

#### 🌟 Parse title

Title is the first line of the file, followed by 2 empty lines

#### 🌟 Pass in output dir as argument

Specify a different output directory, default is `dist`

#### 🌟 Parse Markdown files

Markdown syntax supported:

- bold text \*\*bold\*\*

- hr \-\-\-

```bash
$ dotnet run -- -i sample.md -o pages
```

#### 🌟 Pass in lang as an option

Specify the language of generated .html files. Default is en-CA.

```bash
$ dotnet run -- -i page.txt page.md --lang vi-VN
```

#### 🎉Support static images

Demo: [https://paper-csharp.vercel.app/sample/markdown/Gallery.html](https://paper-csharp.vercel.app/sample/markdown/Gallery.html)

Source file: [Gallery.md](https://github.com/Andrewnt219/paper-csharp/blob/master/sample/markdown/Gallery.md)

Place your images in `static` folder and refer to them in markdowns

```md
![unsplash 2021 collection](unsplash-2021-collection.jpg)
```

## Contributing

For collaboration, see [CONTRIBUTING.md](CONTRIBUTING.md)
