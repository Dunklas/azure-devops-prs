# azure-devops-prs

Lists open pull requests in a given Azure DevOps project.

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
- Windows: `???`
