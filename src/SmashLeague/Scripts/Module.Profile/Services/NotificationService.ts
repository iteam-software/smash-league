
module SmashLeague.Profile {
  'use strict';

  export class NotificationService {

    private _http: ng.IHttpService;

    public static $inject = [
      '$http'
    ];

    constructor(
      http) {

      this._http = http;
    }

    public GetRead(
      ): ng.IHttpPromise<any[]> {

      return this._http.get('/api/notification/read');
    }

    public GetUnread(
      ): ng.IHttpPromise<any[]> {

      return this._http.get('/api/notification/unread');
    }

    public Get(
      ): ng.IHttpPromise<any[]> {

      return this._http.get('/api/notification');
    }

    public Read(
      notification: any): ng.IHttpPromise<any> {

      return this._http.put('/api/notification/read', notification);
    }

    public Delete(
      notification: any): ng.IHttpPromise<{}> {

      return this._http.delete('/api/notification?id=' + notification.NotificationId);
    }

    public static get Factory() {

      var factory = (http) => new NotificationService(http);

      factory.$inject = NotificationService.$inject;

      return factory;
    }
  }

  Application.Module.factory('NotificationService', NotificationService.Factory);
}