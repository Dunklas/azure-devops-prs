# azure-devops-prs

Lists open pull requests in a given Azure DevOps project.

## Installation

Install the tool via the following steps:

1. Clone the repository
2. Navigate to the repository root
3. Run `dotnet pack`
4. Run `dotnet tool install -g azure-devops-prs --add-source=./nupkg`

Next, you need to configure the tool.

## Update

Update is done by uninstall, followed by install.

1. `dotnet tool uninstall azure-devops-prs --global`
2. `dotnet pack`
3. `dotnet tool install -g azure-devops-prs --add-source=./nupkg`

## Configuration

`azure-devops-prs` reads configuration from a json file.

The json file should have the following properties:

```
{
    "url": "https://your-azure-devops-url",
    "pat": "personal-access-token*",
    "project": "project"
}
```

**\*** The personal access token must have read repository permissions

The folder where the file should be located depends on your operating system.

- Linux: `/.config/azure-devops-prs/config.json`
- Windows: `%APPDATA%\azure-devops-prs\config.json`

## Usage

Run `azure-devops-prs` from your terminal of choice.
