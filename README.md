# paper-cli

Static site generator (SSG) made with [.NET](https://dotnet.microsoft.com/).

Sample website: [https://paper-csharp-sample.vercel.app/](https://paper-sample.vercel.app/)

To start running:

1. [Install .NET](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net50)

2. Run

```bash
$ dotnet run -- --help

  -i, --input         Required. Path to file(s)

  -s, --stylesheet    (Default: ./assets/style.css) Path to css file or url

  -o, --output        (Default: ./dist) Path to output directory

  --help              Display this help screen.

  --version           Display version information.
```

## Implemented optional features

#### 🎉 Generate `index.html`

The index file includes paths to all the generated html files (recursively)

```bash
$ dotnet run -- -i dir-with-nested-dirs-and-files
```

#### 🌟 Pass in stylesheet's file OR url as a CLI arg

Content of `.css` files are bundled into all the generated `.html` files

```bash
$ dotnet run -- -i page.txt --stylesheet ./my-style.css
```

#### 🎉 Keep source folder structure

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

#### 🎉 Parse Markdown files

Markdown syntax supports HTML headers bold syntax

```bash
$ dotnet run -- -i sample.md -o pages
```
