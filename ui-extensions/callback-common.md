# Common callback semantics

When talking about callbacks, the term _request_ and _response_ refers to the HTTP message sent from Cinode to the integration and the following response back.

## Common request structure

All callbacks are invoked with these headers.

| Header                | Value                                                     |
| --------------------- | ----------------------------------------------------------|
| `Content-Type`        | `application/vnd.cinode.callback.v1+json`                 |
| `Digest`              | See [security.md](./security.md)                          |
| `X-Cinode-Signature`  | See [security.md](./security.md)                          |
| `X-Cinode-Company-Id` | `companyId` of the company that triggered the callback    |
| `X-Cinode-User-Id`    | `companyUserId` of the user who triggered the callback    |

All callbacks can be verified to ensure it originates from Cinode tamper free. See [security.md](./security.md) for more info.

```http
POST /some/callback/handler/endpoint HTTP/1.1
Content-Type: application/vnd.cinode.callback.v1+json
Digest: sha-256=...
X-Cinode-Signature: ...
X-Cinode-Company-Id: 123
X-Cinode-User-Id: 123
```
```json
{
    // Triggered in company
    "company": {
        "id": 123,
        "name": "Sven Svenssons IT-Firma"
    },

    // Triggered by user.
    "user": {
        "id": 123,
        "email": "sven@svensvensson.se",
        "name": "Sven Svensson",
        "language": "en-GB"
    },

    "action": {
        "event": "submit" // submit, form, data
    },

    "context" : {
        // Identifies the entity this callback is triggered from.
        // Key is the <entity name> correlateing with the extensions configuration json, $.ui.<entity name>.[panels|menu].
        "project": { "id": 123 },

        // Action specific context properties
    }

    // Action specific properties
}
```

### Context

All callbacks have a `context` property. At the very least it contains the Cinode entity identifier, but can also contain other relevant data.

For example, any callback triggered from an `itemActions` in a `table` panel, will contain an additional `item` context property identifying the subject item. See [Table item actions](./configuration.md#table-item-actions).

## Response

Depending on the type of callback, different properties are expected. See specific topics for details.

The bare minimum response is simply a `200 OK` and an empty json object.  
The end-user is notified with a generic success confirmation message.

```http
HTTP/1.1 200 OK
Content-Type: application/json

{}
```

#### 'message' object

A `message` object can be included to customize the confirmation message. A link can also be included to some externally created or updated resource.

The `style` property controls how the message is displayed.

-   `flash` shows the message and link as a non-invasive message. Used when a successful operation don't require the end-users immediate attention.
-   `popup` show the message in a very invasive popup requiring the end-user to confirm the message.

```http
HTTP/1.1 200 OK
Content-Type: application/json
```
```json
{
    "message": {
        "value": { "en": "Thing created!" },
        
        // Optional, default is "flash"
        "style": "popup",

        // Optional
        "link": { 
            "label": {"en": "Show thing" },
            "url": "https://example.com/thing"
        }
    }
}
```

### Handling failures

Errors are communicated back with an HTTP status code indicating the type of error. Depending on the type of action, this will determine the UI behavior. See specific topics for details.

Use the `error` object to customize the error message displayed to the end-user. A generic "Action failed for an unknown reason" error message is shown if not specified. Some actions have specific behaviors for errors.

Respond with a `400 Bad Request` for user errors like invalid configuration or bad input.

```http
HTTP/1.1 400 Bad Request
Content-Type: application/json
```
```json
{
    "error": {
        "value": "Could not share project with #missing-chanel, because channel was not found."
    }
}
```

Respond with any `5xx` for server errors or intermittent errors. The end-user is presented with the error and an option to retry the same action.

```http
HTTP/1.1 503 Service Unavailable
Content-Type: application/json

{
    "error": {
        "value": "Slack is unable to process the request at the moment."
    }
}
```

### HTTP Status codes

-   Status `200 OK`  
    Indicates success.

-   Status `400 Bad Request`
    User error, like invalid app configuration, or failed form validation.

-   Status `5xx`  
    Notify user of error and option to try again.
