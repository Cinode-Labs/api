# Data source callbacks

Used when data is requested, e.g.. when loading and reloading a table panel.

Calls the `dataUrl` and expects a response containing data, or an error message. Payload format depends on the component requesting the data.

## Configuration

```json

{    
    "dataSource": {
        "$type": "data",
        "name": "unique-name",

        "dataUrl": "https://example.com/api/data",
    }
}
```

## Events

### `data`

Invoked when data is requested, i.e., to be displayed in a panel.

- Respond with a `200 OK` response and a `data` object.
- Respond with a `400 Bad Request` response to display a general error in place of the panel content.

Initial load will only contain an empty object.

```http
POST /api/data HTTP/1.1
Content-Type: application/vnd.cinode.callback.v1+json
Digest: sha-256=...
X-Cinode-Signature: ...
X-Cinode-Company-Id: 123
X-Cinode-User-Id: 123

{}
```

Subsequent requests might include `filters` and `pagination` properties.

```http
POST /api/data HTTP/1.1
Content-Type: application/vnd.cinode.callback.v1+json
Digest: sha-256=...
X-Cinode-Signature: ...
X-Cinode-Company-Id: 123
X-Cinode-User-Id: 123

{
    "filters": {
        "recipient": {
            "value": "asdasdasdasdasdas"
        },
        "some-complex-query": {
            "value": "my query value"
        },
        "status": {
            "value": ["new", "completed"]
        }
    },

    "pagination": {
        "cursor": "next page cursor"
    },
}
```

Response must contain a `properties`, and `values` properties. Optionally a `filters` and/or `pagination` property.

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
    "data": {
        // Map of filter elements to define filters.
        "filters": {
            "recipient": {
                "$type": "input",
                "label": {"en": "Recipiant name"},
            },
            "some-complex-query": {
                "$type": "input",
                "label": {"en": "Query"},
            },
            "status": {
                "$type": "multiselect",
                "label": {"en": "Satus"},
                "values": [
                    {"value": "new", "label": "New"},
                    {"value": "completed", "label": "Completed"},
                    {"value": "removed", "label": "Removed"}
                ]
            }
        },

        // Object describing the pagination strategy. 
        "pagination": {
            "$type": "cursor",
            "cursor": "next page cursor"
        },

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
                "template": { 
                    "value": "https://example.com/standard-development-agreement", 
                    "renderAs": { "$type": "link", "label": { "en": "Standard development agreement" } }
                },
                "status": { "value": "Signed" }
            },
            {
                "recipient": { "value": "Bob Boston"  },
                "template": { 
                    "value": "https://example.com/premium-maintenance-sla", 
                    "renderAs": { "$type": "link", "label": { "en": "Premium maintenance SLA" } }
                },
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
