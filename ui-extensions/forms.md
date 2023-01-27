# Forms

## Form definition

A form definition consists of a `title`, `description`, a map of elements called `form`, and a collection of elements `rows`.

```json
{
    "title": "Title of form modal",
    "description": "Description describing the described form",
    "form": {
        "channel": {
            "$type": "select",
            "label": "Channel",
            "description": "Select the channel you want to post the message to.",
            "values": [
                {"label": "#general", "value": "C035JSCTY" },
                {"label": "#development", "value": "C0E9NCBM0" },
                {"label": "#customer-integrations", "value": "C021CS435NE" }
            ]
        },
        "customMessage": {
            "$type": "textarea",
            "label": "Custom message"
        },
        "dance": {
            "$type": "radio-group",
            "label" : "Add :dance: emoji?",
            "values": [
                {"label": "Yes", "value": "yes" },
                {"label": "No", "value": "no" },
            ]
        }
    },
    "rows": [
        { "elements": ["channel", "dance"] },
        { "elements": ["customMessage"] }
    ]
}
```

### Element properties

Properties applicability depends on the element type.

```json
{
    "$type": "", // input, textarea, etc.

    "label": "",
    "description": "",
    "placeholder": "",

    "required": false, // optional, defaults to false
    "readonly": false, // optional, defaults to false

    // Values for multiple choice elements
    "values": [
        { "label": "First value", "value": "first-value" },
        { "label": "Second value", "value": "second-value" }
    ],

    // Initial value of the element. Data type depends on element.
    "default": null,

    // Array of conditions.
    "dependsOn": []
}
```

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
        "default": false
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
            { "label": "Sweden", "value": "sv" },
            { "label": "Norway", "value": "no" },
            { "label": "Finland", "value": "fi" },
            { "label": "Denmark", "value": "dk" }
        ],
        "default": "no"
    },

    "messageToDenmark": {
        "$type": "text",
        "label": "Better luck next life.",
        "dependsOn": [{ "valueIn": { "element": "country", "value": ["sv", "no", "fi"] } }]
    }
}
```

### Element rows

Used for grouping related elements into rows. Each group have a required collection of element names, `elements`.

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

    "rows": {
        { "elements": ["el1", "el2"] },
        { "elements": ["el3", "el4"] },
        { "elements": ["el5", "el6", "el7"] }
        // "el8" and all other non-grouped elements will be displayed last as single element groups.
    }
}
```

## Standard elements

### input

```json
{
    "$type": "input",
    "default": "string"
}
```

### textarea

```json
{
    "$type": "textarea",
    "default": "a long string string"
}
```

### date

```json
{
    "$type": "date",
    "default": "2022-05-11" // ISO 8601
}
```

### time

```json
{
    "$type": "time",
    "default": "12:30:00" // ISO 8601
}
```

### datetime

```json
{
    "$type": "datetime",
    "default": "2022-05-11T12:30:00" // ISO 8601
}
```

### password

```json
{
    "$type": "password",
    "default": ""
}
```

### select

```json
{
    "$type": "select",
    "values": [
        { "label": "First value", "value": "first-value" },
        { "label": "Second value", "value": "second-value" }
    ],
    "default": "first-value" // the "value" from a values tuple.
}
```

### radio-group

```json
{
    "$type": "radio-group",
    "values": [
        { "label": "First value", "value": "first-value" },
        { "label": "Second value", "value": "second-value" }
    ],
    "default": "first-value" // the "value" from a values tuple.
}
```

### checkboxes

```json
{
    "$type": "checkboxes",
    "values": [
        { "label": "First value", "value": "first-value" },
        { "label": "Second value", "value": "second-value" },
        { "label": "Third value", "value": "third-value" }
    ],
    "default": ["first-value", "third-value"] // Array of "value" from a values tuple.
}
```

## Visual elements

These are elements that do not carry any value and are purely intended for structuring your forms visually.

They also respect the `dependsOn` property.

### header

```json
{
    "$type": "header",
    "label": ""
}
```

### text

```json
{
    "$type": "text",
    "label": ""
}
```

### separator

```json
{
    "$type": "separator"
}
```
