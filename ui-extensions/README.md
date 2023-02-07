# Extensions

Cinode Extensions enables third-party integration developers to extend the Cinode platform with interactive features like menu actions and data views.

The only requirement is that your integration is implemented as an AppMarket app.

> **Note**
> Cinode extensions are currently in *early preview* and as such lots of features are still missing.

## Overview

In short, these are the steps needed to implement a Cinode extension,

1. Register an AppMarket app in Cinode, `Administration > Integrations > Apps > Registration`.
2. Implement the OAuth flow and install the app into your Cinode company.
3. Declare an extension in your app's [extension configuration](configuration.md), `.. > Registration > "Your App" > Extensibility`.
4. Implement the extension callback HTTP endpoint in your app.
5. **Highly recommended**: Implement request signature verification.

## Reference guide

- [Configuration](configuration.md)
- [Callback handling](callbacks.md)
  - [Security](security.md)
- [Actions](callback-actions.md)
  - [Basic action](callback-actions.md#basic-callback)
  - [Form action](callback-actions.md#form-callback)
    - [Form definition](forms.md)
  - [Data source](callback-data.md)
- [Handling translations](translations.md)
