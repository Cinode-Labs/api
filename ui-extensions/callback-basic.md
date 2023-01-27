# Basic callback (action)

Simply calls the `submitUrl` and expects a success or error message in response.

## Configuration

```json
{
    "action": {
        "$type": "basic",
        "name": "unique-name",

        "submitUrl": "https://example.com/api/basic/submit"
    }
}
```

## Events

### `submit`

#### Request

*No specific properties*

#### Response

*No specific response properties*