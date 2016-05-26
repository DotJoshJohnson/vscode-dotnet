> :bangbang: **Please note, this product is still in alpha.**

# VSCode.NET
VSCode.NET is a Visual Studio Code language server for .NET/.NET Core. It is designed to be fully compliant with the [language server protocol](https://github.com/Microsoft/vscode-languageserver-protocol) and compatible with Microsoft's [NodeJS language server client implementation](https://github.com/Microsoft/vscode-languageserver-node/tree/master/client).

## Getting Started
Creating a language server using VSCode.NET is pretty simple:

1. Create a new VS Code extension based on the [language server example](https://code.visualstudio.com/docs/extensions/example-language-server) outlined in the VS Code documentation.

2. In `extension.ts`, change the contents of the `activate(context: ExtensionContext)` function to the following (making changes as needded for your extension):
  ```typescript
  export function activate(context: ExtensionContext) {
      let clientOptions: LanguageClientOptions = {
          documentSelector: ['plaintexet'],
          synchronize: {
              configurationSection: 'yourConfigSection',
              fileEvents: workspace.createFileSystemWatcher('**/.clientrc')
          }
      };
      
      let disposable = new LanguageClient('VSCode.NET', {
          command: 'path/to/your/server/executable.exe'
      }, clientOptions).start();
  }
  ```
  
3. Create a new .NET or .NET Core project and reference the `VSCode` package.

4. Spin up a language server:
  ```csharp
  using VSCode;
  using VSCode.Editor;
  
  namespace MyApp
  {
      class Program
      {
          static void Main()
          {
              using (LanguageServer server = new LanguageServer())
              {
                  server.Start();
                  server.WaitForState(LanguageServerState.Started);
                  
                  server.Editor.ShowMessage(MessageType.Info, "Hello from .NET!");
              }
          }
      }
  }
  ```
  
5. Read the [API Documentation](http://dotjoshjohnson.github.io/vscode-dotnet/api-docs) to learn more!

## FAQ
* **Can I use this for my VS Code extension?**<br />
  Of course! It should be noted, however, that VSCode.NET is currently in the early alpha stages. Breaking changes to the API could happen until things mature a bit.

* **What operating systems are supported?**<br />
  * Windows 8, 8.1, 10, Server 2012, and Server 2016 (via `.NET 4.5/4.5.1/4.5.2` or `.NET Core 1.0`)
  * Mac OS X (via `.NET Core 1.0`)
  * Linux (via `.NET Core 1.0`)
