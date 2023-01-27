# Data callback (dataSource)

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

-   Respond with a `200 OK` response and a `data` object.

-   Respond with a `400 Bad Request` response to display a general error in place of the panel content.

-   Respond with a `5xx` response to display a general error in place of the panel content.

#### Request

*No specific properties*

#### Response

**200 OK**

```json
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