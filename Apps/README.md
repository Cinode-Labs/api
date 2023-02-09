# AppMarket

As an integration developer, you may opt-in to design your Cinode integration as an AppMarket App.

Apps have access to some extra features like

- AppMarket page, enabling distribution as a Turn Key integration.
- No need to manage a dedicated/personal integration account/tokens.
- Extend the Cinode user interface with the [Cinode Extensions](../Extensions/README.md) framework.

## Installing an app

> **TL;DR**  
> Cinode Apps implement *OAuth 2.0 authorization code grant* flow, with an implicit for exchanging access tokens as part of an installation process.
> You should be able to use any compatible tool or framework as part of your implementation.

### 1. Authorizing an app

The user initiates the authorization process with the *Install* link from your app market page or by navigating to the your *Authorization URL*.

`https://app.cinode.com/_application/authorize?client_id=<app client id>&redirect_url=<app callback url>`

If a user decides to install your app, the user is redirected to your *Callback URL*, together with a short lived *Authorization Code* as query parameter, `code`.

`https://your-callback-url/?code=<authorization code>`

### 2. Finalize authorization

Your app then needs to exchange the authorization `code` for a `access_token` and `refresh_token` from our *OAuth Token endpoint*.

This is done by sending a POST request to `https://api.cinode.com/oauth/token`.  
Use `Authorization: Basic` with your *Client Id* as username, and *Client Secret* as password.  
Body has to include the following properties encoded as `application/x-www-form-urlencoded`:

| Parameters   | Value                                     |
| ------------ | ----------------------------------------- |
| grant_type   | 'authorization_code'                      |
| code         | `code` from the incoming query parameter  |
| redirect_uri | *Callback URL* from your app registration |

```plaintext
POST /oauth/token HTTP/1.1
Authorization: Basic <base64(ClientId:ClientSecret)
Content-Type: application/x-www-form-urlencoded

grant_type=authorization_code
   &code=<Authorization Code>
   &redirect_uri=<Callback URL from app registration>
```

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
   "access_token": "<access token>",
   "refresh_token": "<refresh token>",
   "company_id": "<target company id>"
}

```

The response contains `access_token` and `refresh_token`, and for convenience, `company_id`. *Store these tokens in a safe place.* 

Your app is now authorized and ready to go.

### 3. Refresh access token

The `access_token` will expires after **1 hour**.  
When this happens you'll need to get a new access token using a refresh_token.

This is done by sending a POST request to `https://api.cinode.com/oauth/token`.  
Use `Authorization: Basic` with your *Client Id* as username, and *Client Secret* as password.  
Body has to include the following properties encoded as `application/x-www-form-urlencoded`:

| Parameters    | Value                                     |
| ------------- | ----------------------------------------- |
| grant_type    | 'refresh_token'                           |
| refresh_token | `refresh_token`                           |

```plaintext
POST /oauth/token HTTP/1.1
Authorization: Basic <base64(ClientId:ClientSecret)
Content-Type: application/x-www-form-urlencoded

grant_type=refresh_token
   &refresh_token=<refresh_token>
```

The response contains new `access_token` and `refresh_token`. Replace your old tokens.
