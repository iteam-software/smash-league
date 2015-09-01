
module SmashLeague {

  export interface IAutocompleteScope extends ng.IScope {

    Results: any[];
    Load: (arg: string) => ng.IHttpPromise<any[]>;
  }
}