# Extensions configuration

The extensions configuration is the entry point to Cinode extensions. It's written in JSON and declares what parts of Cinode you want to extend, and how to communicate with your implementation.

You'll find your extensions configuration with app's registration, `Administration > Integrations > Apps > Registration`.

## Base configuration

> This schema is also available as a JSONSchema - [schema.json](schema.json).

The configuration structure declares what extension points you want to extend, and how they are extended.

This example demonstrates currently implemented extension points.

```json
{
    
    "ui": { 
        "project": { 
            "menu": [],
            "panels": {
                "overview": []

            }
        }
    }
}
```

| Property                          | Type                                   | Description                                  |
| --------------------------------- | -------------------------------------- | -------------------------------------------- |
| `ui[entity-name].menu`            | Array of [Menu actions](#menu-actions) | Actions to add to the entity menu menu       |
| `ui[entity-name].panels.overview` | Array of [Panels](#panels)             | Panels displayed on the entity overview page |

## Menu actions

```json
{
    "label": {
        "en": "English label",
        "sv": "Svensk etikett"
    },
    "name": "unique-name",
    "icon": "unspecified",
    "style": "neutral",
    "action": {}
}
```

| Property | Type                                   | Description                                                             |
| -------- | -------------------------------------- | ----------------------------------------------------------------------- |
| `name`   | *Unique name*                          | Unique internal menu item name/identifier                               |
| `label`  | *Localized string*                     |                                                                         |
| `icon`   | `add`, `delete`, `edit`, `unspecified` | Pre-defined icon to visually communicate intent. Default `unspecified`. |
| `style`  | `positive`, `warning`, `neutral`       | Visual highlight to communicate impact. Default `neutral`.              |
| `action` | *Action callback*                      |                                                                         |

## Panels

### Table panel

```json
{
    "$type": "table",
    "label": { "en": "English label" },
    "description": { "en": "English description" },
    
    "name": "unique-name",
    "dataSource": {},
    "primaryAction": {},
    "itemActions": []
}
```

| Property        | Type                                       | Description                                   |
| --------------- | ------------------------------------------ | --------------------------------------------- |
| `$type`         | "table"                                    |                                               |
| `name`          | *Unique name*                              | Unique internal panel name/identifier         |
| `label`         | *Localized string*                         |                                               |
| `description`   | *Localized string*                         |                                               |
| `dataSource`    | [Data source callback](./callback-data.md) |                                               |
| `primaryAction` | *Action callback*                          |                                               |
| `itemActions`   | Array of [Menu actions](#menu-actions)     | See [Table item actions](#table-item-actions) |

### Table item actions

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
