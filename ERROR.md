# Error
Sometimes things goes wrong, maybe it's your fault or maybe it's ours. The `status code` tells you who to blame.

## 4xx Client error
It's your fault! Hopefully there will be some error messages telling you what went wrong, probably you missed a required field. Check the `errors` property on the response.

## 5xx Server error
It's our fault! If the error keeps repeating we would appreciate if you could tell us and provide the `Correlation Id` provided in the response, this will help us investigate the problem.