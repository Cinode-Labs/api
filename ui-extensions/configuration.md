# Extensions configuration

The extensions configuration is written in JSON and declares what parts of Cinode you want to extend, and where your integration is hosted.

The JSON configuration is located in under the `Extensibility` tab on your apps registration page.

## Schema

> This schema is available in as a JSONSchema: [schema.json](schema.json).

The configuration structure declares what extension points you want to extend, and how they are extended.

This example demonstrates currently implemented extension points.

```json
{
    
    "ui": { 

        // Extend the project page with UI elements.
        "project": { 

            // Array of `Menu actions`; actions accessible from the main entity page menu.
            "menu": [], 

            "panels": {
      
                // Panels under the overview tab.
                "overview": []

            }
        }
    }
}
```

### Menu actions

```json
{
    "label": {
        "en": "English label",
        "sv": "Svensk etikett"
    },

    "name": "unique-name",
    
    // Show a pre-defined icon to futher communicate intent, if applicable.
    // Posible values: add, delete, edit, or unspecified (default)
    "icon": "unspecified",

    // Visually highlight your menu action to further comunicate impact, if applicable.
    // Posible values: positive, warning, neutral (default)
    "style": "neutral",

    // `Action callback`; defines what type of behaviour this actin implements, and where to invoke the extension via HTTP.
    "action": {}
}
```

### Table panel 

```json
{
    // Must be "table"
    "$type": "table",
    "label": { "en": "English label" },
    "description": { "en": "English description" },
    
    "name": "unique-name",

    // `Data callback`; defined how data is fetched.
    "dataSource": {},
    
    // `Menu action`; if defined, renders a button next to the panel label.
    "primaryAction": {},

    // Array of `Menu actions`; available for each item in the table.
    "itemActions": []
}
```

#### Table item actions

Triggered item action callbacks will contain an additional property `context.item`.

The item context property contains the entire row item as-is from the data source response.

For example, consider the following data response:

```json
{
    "data" {
        "properties": {...},
        "values": [
            { "myValue": 2, "someOtherValue": "abc" }
        ]
    }
}
```

Any `itemActions` triggered on this item will produce a context like this: 

```json
{
    "context": {
        "project": { "id": ... },
        "item": { "myValue": 2, "someOtherValue": "abc" } 
    }
}
```
