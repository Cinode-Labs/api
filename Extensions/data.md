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

## Filters

A filter should have its `value` specified in the case of a pre-filtered data set.

- text
- select

### text

```
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

### cursor

Cursor based pagination only used a cursor value or a "next page token" to signal the availability of more data. The cursor can be any arbitary value, as long as it can be used to retreive the next page.

```json
{
    "$type": "cursor",

    "cusrsor": "some value for next page"
}
```