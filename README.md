# paper-cli

Static site generator (SSG) made with [.NET](https://dotnet.microsoft.com/).

Sample website: [https://paper-csharp-sample.vercel.app/](https://paper-csharp.vercel.app/)

```bash
$ dotnet run -- --help

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
$ dotnet run -- -i dir-with-nested-dirs-and-files
```

#### 🌟 Pass in stylesheet's file OR url as a CLI arg

Content of `.css` files are bundled into all the generated `.html` files

```bash
$ dotnet run -- -i page.txt --stylesheet ./my-style.css
```

#### 🌟 Keep source folder structure

If a directory is passed as `--input`, `dist` keeps the structure of the source dir

```bash
$ donet run -- -i sample-dir

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
