# Forms

## Form definition

A form definition consists of a `title`, `description`, a map of elements called `form`, and a collection of elements `rows`.

```json
{
    "title": { "en": "Title of form modal" },
    "description": { "en": "Description describing the described form" },
    "form": {
        "channel": {
            "$type": "select",
            "label": { "en": "Channel" },
            "description": { "en": "Select the channel you want to post the message to." },
            "values": [
                { "label": { "en": "#general" }, "value": "C035JSCTY" },
                { "label": { "en": "#development" }, "value": "C0E9NCBM0" },
                { "label": { "en": "#customer-integrations" }, "value": "C021CS435NE" }
            ]
        },
        "customMessage": {
            "$type": "textarea",
            "label": { "en": "Custom message" }
        },
        "dance": {
            "$type": "radio-group",
            "label": { "en": "Add :dance: emoji?" },
            "values": [
                { "label": { "en": "Yes" }, "value": "yes" },
                { "label": { "en": "No" }, "value": "no" }
            ]
        }
    },
    "rows": [
        { "elements": ["channel", "dance"] },
        { "elements": ["customMessage"] }
    ]
}
```

### Element rows

Used for grouping related `elements` into rows.

```json
{
    "form": {
        "el1": {...},
        "el2": {...},
        "el3": {...},
        "el4": {...},
        "el5": {...},
        "el6": {...},
        "el7": {...},
        "el8": {...}
    },

    "rows": [
        { "elements": ["el1", "el2"] },
        { "elements": ["el3", "el4"] },
        { "elements": ["el5", "el6", "el7"] }
        // "el8" and all other non-grouped elements will be displayed last as single element groups.
    ]
}
```

## Elements

**Common element properties**

> See [Standard elements](#standard-elements) and [Visual elements](#visual-elements)

|        Name | Description                                                         | Required |
| ----------: | ------------------------------------------------------------------- | :------: |
|       $type | `string` - Element type name                                        |  **X**   |
|       label | `LocalizedString`                                                   |  **X**   |
| description | `LocalizedString`                                                   |          |
| placeholder | `LocalizedString`                                                   |          |
|    required | `bool` - Requires a user to provide a `value` <sup>1</sup>          |          |
|    readonly | `bool` - Do not allow user to modify `value` <sup>1</sup>           |          |
|   dependsOn | `DependsOnExpression` [Element dependencies](#element-dependencies) |          |
|       value | `string` - Some elements assumes special formatting                 |          |

> <sup>1</sup> Always validate user-input

### Standard elements

- input
- textarea
- date
- time
- datetime
- password
- select
- radioGroup
- checkboxes

#### date, time, and datetime

```json
{
    "$type": "date",
    "value": "2022-05-11" // ISO 8601
}
```

```json
{
    "$type": "time",
    "value": "12:30:00" // ISO 8601
}
```

```json
{
    "$type": "datetime",
    "value": "2022-05-11T12:30:00" // ISO 8601
}
```

#### select, radioGroup, and checkboxes

```json
{
    "$type": "select", // or "radioGroup",
    "values": [
        { "label": { "en": "First value" }, "value": "first-value" },
        { "label": { "en": "Second value" }, "value": "second-value" }
    ],
    "value": "first-value" // the "value" from a values tuple.
}
```

```json
{
    "$type": "checkboxes",
    "values": [
        { "label": { "en": "First value" }, "value": "first-value" },
        { "label": { "en": "Second value" }, "value": "second-value" },
        { "label": { "en": "Third value" }, "value": "third-value" }
    ],
    "value": ["first-value", "third-value"] // Array of "value" from a values tuple.
}
```

### Visual elements

These are elements that do not carry any value and are purely intended for structuring your forms visually.

- header
- separator

They also respect the `dependsOn` property.

### Element dependencies

Some scenarios call for dynamically changing parts of the form depending on the state of other fields.  
The `dependsOn` property allows you to hide elements until all criteria are met.

Elements hidden by `dependsOn`, are treated as `{ "readonly": true, "required": false }`.  
Their values are also excluded in the `submit` callback.

#### valueEquals

Element value must equal the criteria value.

```json
{
    "separateBillingAddress": {
        "$type": "checkbox",
        "value": false
    },

    "shippingAddress": {
        "$type": "textarea"
    },

    "billingAddress": {
        "$type": "textarea",
        "dependsOn": [{ "valueEquals": { "element": "separateBillingAddress", "value": true } }]
    }
}
```

#### valueIn

Element value must equal one of the criteria values.

```json
{
    "country": {
        "$type": "select",
        "values": [
            { "label": { "en": "Sweden" }, "value": "sv" },
            { "label": { "en": "Norway" }, "value": "no" },
            { "label": { "en": "Finland" }, "value": "fi" },
            { "label": { "en": "Denmark" }, "value": "dk" },
            { "label": { "en": "United Kingdom" }, "value": "uk" },
            { "label": { "en": "United States" }, "value": "us" }
        ]
    },

    "extraDetails": {
        "$type": "text",
        "label": { "en": "Extra information" },
        "description": { "en": "Countries outside of Scandinavia are required to provide more details" },
        "dependsOn": [{ "valueIn": { "element": "country", "value": ["sv", "no", "fi", "dk"] } }]
    }
}
```
