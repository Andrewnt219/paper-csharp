### Set up dev environment

1. [Install .NET Core for Visual Studio Code](https://code.visualstudio.com/docs/languages/dotnet)

2. Install dependencies

```bash
$ yarn
```

3. Install toolings

```bash
$ yarn setup
```

### Console development

Run in debug mode (F5) to automatically generate output in `dist`. By default, the program run with these arguments

```bash
$ dotnet run -- -i sample
```

You can customize this by editing `configurations.args` inside file `.vscode/launch.json`.

### Frontend development

To see website generated from `dist`, run

```bash
$ yarn dev
```

### Project structure

```bash
+-- assets        # static assets internally used by the application
|
+-- build         # generated htmls used for demo deployment
|
+-- dist          # default output directory
|
+-- modules       # features of the application, each directory is one feature
|
+-- sample        # default input files
|
+-- static        # used for static assets of users
|
+-- Program.cs    # entry point

```

### Linting

```bash
$ yarn format
```
