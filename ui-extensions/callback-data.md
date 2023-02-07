# Data source callbacks

Used when data is requested, eg. when loading and reloading a table panel.

Calls the `dataUrl` and expects a response containing data, or an error message. Payload format depends on the component requesting the data.

## Configuration

```json
{
    "$type": "data",
    "name": "unique-name",

    "dataUrl": "https://example.com/api/data",
}
```

## Events

### `data`

Invoked when data is requested, i.e., to be displayed in a panel.

- Respond with a `200 OK` response and a `data` object.
- Respond with a `400 Bad Request` response to display a general error in place of the panel content.

```http
POST /api/data HTTP/1.1
Content-Type: application/vnd.cinode.callback.v1+json
Digest: sha-256=...
X-Cinode-Signature: ...
X-Cinode-Company-Id: 123
X-Cinode-User-Id: 123

{}
```

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
    "data": {
        // Map of property names to property metadata
        "properties": {
            "recipient": { "label": "Recipient" },
            "template": { "label": "Template" },
            "status": { "label": "Status" }
        },

        // Array or data entry objects
        // Each object is a map of property names to property values
        "values": [
            {
                "recipient": { "value": "Bob Boston" },
                "template": { "value": "Standard development agreement" },
                "status": { "value": "Signed" }
            },
            {
                "recipient": { "value": "Bob Boston" },
                "template": { "value": "Premium maintenance SLA" },
                "status": { "value": "Sent" }
            }
        ]
    }
}
```

```http
HTTP/1.1 400 OK
Content-Type: application/json

{
    "error": {
        "value": { "en": "Something went wrong" },
    }
}
```
