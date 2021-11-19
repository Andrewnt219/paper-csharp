### Set up dev environment

1. [Install .NET SDK 6](https://dotnet.microsoft.com/download/dotnet/6.0)

2. [Install .NET Core Runtime 5](https://dotnet.microsoft.com/download/dotnet/5.0)

3. Install dependencies

```bash
yarn
```

4. Install toolings

```bash
yarn setup
```

### Console development

Console app is located at "/Generator"

```bash
cd ./Generator

dotnet run -- -i sample
```

### Frontend development

To see website generated from `dist`, run

```bash
yarn dev
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

### Formatting

```bash
yarn format
```

### Linting

```bash
yarn lint
```

### Testing

To run all tests

```bash
yarn test
```

To run specific tests

```bash
# Run all tests inside GeneratorTesters
yarn test:filter GeneratorTester

# Run all tests include the text 'RunWith'
yarn test:filter RunWith
```
