# Cinode.Samples

Samples for authentication and usage of Cinode's API. Theese samples may or may not update according to updates in the API.

## AppSettings

In order to use this sample you must add some appsettings, either add the following to `appsettings.json` or via `secrets.json` (recommended) in one of the following places:
```
Windows: %APPDATA%\microsoft\UserSecrets\cinode.samples\secrets.json
Linux: ~/.microsoft/usersecrets/cinode.samples/secrets.json
macOS: ~/.microsoft/usersecrets/cinode.samples/secrets.json
```

```JSON
{
 "Api": {
        "BaseUrl": "https://api.cinode.com",
        "Version" : "0.1" 
    },
	"Token": {
		"BaseUrl": "https://api.cinode.com",
		"TokenEndpoint": "/token",
		"TokenRefreshEndpoint":"/token/refresh",
		"AccessId": "[ACCESSID].app.cinode.com",
		"AccessSecret":"[ACCESSSECRET]"
	}
}
```

`[ACCESSID]` and `[ACCESSSECRET]` can be created under `Account` when you are logged in.

## LaunchSetting

It can also help to add the following `launchSetting.json` under `properties`.

```JSON
{
    "Cinode.Samples.Search": {
        "commandName": "Project",
        "environmentVariables": {
            "ASPNETCORE_ENVIRONMENT": "Development",
            "ASPNETCORE_URLS": "http://localhost:9000"
        }
    },
    "profiles": {
        "Cinode.Samples.Search": {
            "commandName": "Project",
            "environmentVariables": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "applicationUrl": "http://localhost:9000"
        }
    }
}

```