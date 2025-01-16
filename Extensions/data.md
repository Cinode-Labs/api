# Data

## Table definition

A table definition defines each column as `properties`, rows as `values`, optional filters as `filters` and an optional pagination strategy as `pagination`

```json
{
    "data": {
        "filter": { "filter-name": { "$type": "..." } },
        "pagination": { "$type": "..." },
        "properties": { "property-name": {} },
        "values": [ { "property-name": { "value": "..." } } ]
    }
}
```

## Values

```json
[
    { "value": "my simple value, displayed as the label" }
    { "value": "https://example.com", "renderAs": { "$type": "link", "label": { "en": "Example" } } }
]
```

## Values renderAs

Specify how the value should be presented. The value will be presented as-is if not specified.

- link

### link

```json
{
    "value": "link href", // Value becomes the href
    "renderAs": {
        "$type": "link",
        "label": { "en": "My label" }
    }
}
```

## Filters

A filter should have its `value` specified in the case of a pre-filtered data set.

- text
- select

### text

```json
{
    "$type": "text",
    "label": {"en": "Query"},

    // Optional
    "value": "some value"
}
```

### select

Predefined filter values. Multiple values can be selected.

```json
{
    "$type": "select",
    "label": {"en": "Color"},
    "values": [
        { "label": { "en": "Red", }, "value": "red" },
        { "label": { "en": "Green", }, "value": "green" },
        { "label": { "en": "Blue", }, "value": "blue" }
    ],

    // Optional
    "value": [ "green", "blue" ]
}
```

## Pagination

Specify the pagination strategy. Shows all data if not specified.

- cursor

### cursor

Cursor based pagination only used a cursor value or a "next page token" to signal the availability of more data. The cursor can be any arbitary value, as long as it can be used to retreive the next page.

```json
{
    "$type": "cursor",

    "cusrsor": "some value for next page"
}
```