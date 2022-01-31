import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { IBaseService } from '../interfaces/ibaseservice';
import config from '../../../config.json';

@Injectable({
  providedIn: 'root'
})

export class BaseService<TEntity> implements IBaseService {
  constructor(public http: HttpClient, @Inject(String) public Service: string) {

  }


  public httpOptions() {
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
    };
    return httpOptions;
  }

  private httpStringOptions() {
    let httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      }),
      params: {

      },
      responseType: 'text/plain' as 'json',
      observe: 'response' as 'body'
    };
    return httpOptions;
  }

  public async GetAll(): Promise<TEntity[]> {

    return await this.http.get<TEntity[]>(config.BaseURL + this.Service, this.httpOptions()).toPromise();
  }

  public async Get(id: string): Promise<TEntity> {
    return await this.http.get<TEntity>(config.BaseURL + this.Service + '/Get/?id=' + id, this.httpOptions()).toPromise();
  }

  public async Post(entity: any): Promise<TEntity> {
    return await this.http.post<TEntity>(config.BaseURL + this.Service, entity, this.httpOptions()).toPromise();
  }

  public async Put(id: string, entity: any): Promise<any> {
    return await this.http.put(config.BaseURL + this.Service + '/?id=' + id, entity, this.httpOptions()).toPromise();
  }

  public async Delete(id: string): Promise<TEntity> {
    return await this.http.delete<TEntity>(config.BaseURL + this.Service + '/' + id, this.httpOptions()).toPromise();
  }
}
