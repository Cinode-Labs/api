# Extensions configuration

The extensions configuration is written in JSON and declares what parts of Cinode you want to extend, and where you've implemented your callbacks driving your extensions.

Configuration is static and declared with along side your application registration.

## Schema

> This schema is available in as a JSONSchema: [schema.json](schema.json).

The configuration structure declares what extension points you want to extend, and how they are extended.

This example demonstrates currently implemented extension points.

```json
{
    // Extend Cinode with interactive UI elements.
    "ui": { 

        // Specifically the project pages. 
        "project": { 

            // Array of `Menu actions`; actions accessible from the main page menu.
            "menu": [], 

            // Map of tabs/sub-pages and panels to add.
            "panels": {
                
                // Array of `Panels`; extending the "overview" sub-page.
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
    "action": {},
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

Triggered item action callbacks will contain an additional `context` property called `item`.

The item context property contains the entire row item as-is from the data source response.

For example, given the following data response, and an action triggered on the second item:

```json
{
    "data" {
        "properties": {...},
        "values": [
            { "myValue": 1, "someOtherValue": "123" }, 
            { "myValue": 2, "someOtherValue": "abc" } // <-- Action triggerd on this item
        ]
    }
}
```

Results in this context:
```json
{
    "context": {
        "project": { ... }, // The common entity identifier
        "item": { "myValue": 2, "someOtherValue": "abc" } 
    }
}
```
