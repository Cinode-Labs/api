# Callback semantics

Extension behaviors are implemented via HTTP callbacks.

## Callback request

All callbacks are invoked with these headers.

| Header                | Value                                                     |
| --------------------- | ----------------------------------------------------------|
| `Content-Type`        | `application/vnd.cinode.callback.v1+json`                 |
| `Digest`              | See [security.md](./security.md)                          |
| `X-Cinode-Signature`  | See [security.md](./security.md)                          |
| `X-Cinode-Company-Id` | `companyId` of the company that triggered the callback    |
| `X-Cinode-User-Id`    | `companyUserId` of the user who triggered the callback    |

All callbacks can be verified to ensure it originates tamper-free from Cinode. See [security.md](./security.md) for more info.

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
    "company": {
        "id": 123,
        "name": "Sven Svenssons IT-Firma"
    },
    "user": {
        "id": 123,
        "email": "sven@svensvensson.se",
        "name": "Sven Svensson"
    },
    "context" : {
        "project": { "id": 123 },
    }
}
```

### Common callback properties

| Property               | Description                                                     |
| ---------------------- | --------------------------------------------------------------- |
| `company`              | The company (tenant) the callback was triggered from.           |
| `user`                 | User of said company who triggered the callback.                |
| `context[entity-name]` | Subject identifiers. *entity-name* depends on the subject type. |

### Context

All callbacks have a `context` property. At the very least it contains the Cinode entity identifier, but can also contain other relevant data.

For example, any callback triggered from an `itemActions` in a `table` panel, will contain an additional `item` context property identifying the subject item. See [Table item actions](./configuration.md#table-item-actions).

## Callback response

Responses must be formatted as JSON, and include a `Content-Type: application/json` header. HTTP status codes indicate success or failure.

| HTTP Status       | Description                                                        |
| ----------------- | ------------------------------------------------------------------ |
| `200 OK`          | *Success*.                                                         |
| `400 Bad Request` | *User error*. E.g. app not configured, or invalid user/form input. |
| `5xx`             | *Fatal error*. Response body is ignored.                           |

Depending on the type of callback, different properties are expected. See specific topics for details.

### Responding with Success

For `200 OK` responses.

A `message` object can be included to customize the confirmation message. Use `message.style` to controls how the message is displayed.

- `flash` shows the message and link as a non-invasive message, Used when a successful operation doesn't require the end-user's immediate attention. 
- `popup` shows the message in a very invasive popup requiring the end-user to confirm the message.

#### 'message' properties

| Property     | Type                 | Description                           |
| ------------ | -------------------- | ------------------------------------- |
| `value`      | Localized string     | Message to display                    |
| `style`      | `flash`, `popup`     | Defaults to `flash`                   |
| `link`       | Optional link object | Eg. created/updated resource location |
| `link.label` | Localized string     |                                       |
| `link.url`   | URL                  |                                       |

```http
HTTP/1.1 200 OK
Content-Type: application/json
```
```json
{
    "message": {
        "value": { "en": "Thing created!" },
        
        "style": "popup",

        "link": { 
            "label": {"en": "Show thing" },
            "url": "https://example.com/thing"
        }
    }
}
```

### Responding with Failure

> `5xx` responses are assumed to be out of the app's control and treated as intermittent errors. 
> Request will be retried at least once before notifying the user.

Errors are communicated back with an HTTP status code indicating the type of error. The specific UI behavior depends on the type of action being performed. See specific topics for details.

A `400 Bad Request` response indicates a user error and may include an `error` property to customize the error message show to the users. A generic error message is shown if not specified. It's *always recommended* to communicate the issue and how to resolve it.

#### 'error' properties

| Property | Type             | Description      |
| -------- | ---------------- | ---------------- |
| `value`  | Localized string | Error to display |


```http
HTTP/1.1 400 Bad Request
Content-Type: application/json
```

```json
{
    "error": {
        "value": { "en": "Could not share project with #missing-chanel - Channel was not found." }
    }
}
```
