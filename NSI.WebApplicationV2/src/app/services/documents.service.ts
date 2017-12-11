import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

import {Document,
        DocumentDetails,
        DocumentQuery} from '../pages/documents/models/index.model';

import { MDD } from '../pages/documents/models/mockDocumentDetails';

@Injectable()
export class DocumentsService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/documents/';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getDocuments(): Observable<any> {
    // return Observable.create( observer => {
    //   observer.next(MDD);
    //   observer.complete();
    // });
    return this.http.get(this._url, {headers: this.headers});
  }

  getDocumentsWithPaging(queryModel: DocumentQuery): Observable<any> {
    let body = JSON.stringify(queryModel);
    return this.http.post(this._url, body, {headers: this.headers});
  }

  getDocumentById(documentId: number): Observable<any> {
    return this.http.get(this._url + documentId, {headers: this.headers});    
  }
  
  postDocument(document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.post(this._url, body, {headers: this.headers});
  }

  putDocument(document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.post(this._url, body, {headers: this.headers});
  }

  deleteDocument(document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.post(this._url, body, {headers: this.headers});
  }
}
