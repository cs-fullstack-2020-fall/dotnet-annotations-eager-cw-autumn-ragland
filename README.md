# 200902-dotnet-annotations-eager-cw
Data Annotations and eager loading

## Use the provided starter MVC project as a guide when creating a *new* MVC project
* Run and apply migrations
* Verify database created
* Verify application launches

### Actors and Movies

#### Actor
```
id - int, required
ActorName - string, required
Movies - List<Movie>
```

#### Movie
```
id - int, required
MovieTitle - string, required
MovieDescription - string, required, min length 50, max length 1000
MovieRating - int, range 1 - 5
```

### Implement Create and Read endpoints
* Adding a new Movie should require a Actor to tie the Movie to
  * Do an Actor lookup on ID passed in, return error if Actor not found
* Get Actor endpoint should also return all Movies for each Actor and display in a View
* Return list of validation failures on failure when adding new Movie or Actor and display in Postman

### Challenge
Implement Actor List View that will display all actors and all movies

### Extra Code 4 U: 
* Use this method for handling validation errors and return in response visible in Postman for any validation errors while creating a `Actor` or a `Movie`
```
        public static List<string> GetErrorListFromModelState
                                              (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
```        


