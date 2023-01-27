# Form callback (action)

A two-step callback to dynamically present form and validate/process user input.

1. Call `formUrl` to retreive a form definition and render the form accordingly.
2. The user then submits the form and a call is done to `submitUrl` with the user provided values. Expects either a success or validation error message, allowing the user to currect any issues if required.

## Configuration

```json
{
    "action": {
        "$type": "form",
        "name": "unique-name",

        "formUrl": "https://example.com/api/form/definition",
        "submitUrl": "https://example.com/api/form/submit"
    }
}
```

## Events

### `form`

Fetch the form definition.

#### Request

*No specific properties*

#### Response

See [form.md](forms.md) for schema details.

**200 OK**

```json
{
    "title": { "en": "My form" },
    "description": { "en": "This is my form" },
    "form": {
        "myInput": {
            "$type": "textarea",
            "label": { "en": "My input" }
        },
        "mySecondInput": {
            "$type": "textarea",
            "label": { "en": "My second input" }
        }
    },
    
    "rows": {
        { "elements": ["myInput", "mySecondInput"] },
    }
}
```

### `submit`

When the end-user submits the form the `submit` callback event is executed, containing an `input` map with input values corresponding to each field.

-   Respond with a `200 OK` response if the input is valid and was successfully processed.

-   Respond with a `400 Bad Request` if the input is not valid.

-   Respond with a `5xx` response to display a general error. The form will still be visible and can be re-submitted to try again.

#### Request

```json
{
    // Map of element names to input values.
    "input": {
        "myInput": { "value": "some input value" },
        "mySecondInput": { "value": "some other input value" }
    }
}
```

#### Response

**200 OK**

Valid and able to process.

```json
{
    "message": {
        "value": { "en": "Sucess!" }
    }
}
```

**400 Bad Request**

Validation error.

```json
{
    // Error property will also be presented to the end-user.
    "error": {
        "value": { "en": "Some non-specific error" }
    },

    "validation": {
        "mySecondInput": { 
            "error": { "en": "Some error about 'mySecondInput'" } 
        }
    }
}
```