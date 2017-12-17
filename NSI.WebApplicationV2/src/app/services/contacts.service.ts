import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Contact} from "../pages/contacts/new-contact/contact";
@Injectable()
export class ContactsService {

  private readonly _url: string;

  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getContacts(params?: any): Observable<any> {
    return this.http.get(`${this._url}/api/contacts`);
  }

  editContact(body: any): Observable<any> {
    const contact = {
      FirsttName: body.firsttName,
      LastName: body.lastName,
      Phones: body.phones,
      Emails: body.emails,
      AddresId: 1,
      CreatedByUserId: 6
    };

    return this.http.put(`${this._url}/api/contacts/` + body.contact1, contact);
  }

  deleteContact(params?: number): Observable<any> {
    return this.http.delete(`${this._url}/api/contacts/` + params.toString());
  }

  postContact(contact: Contact): Observable<any> {
    const body = JSON.stringify(contact);
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    console.log(body);
    return this.http.post(`${this._url}/api/contacts`, body, {headers: headers});

  }

  getPagedContacts(pageSize: number, pageNumber: number, searchString: string,
                   searchColumn: string, sortOrder: string) {
    let uri = 'pageSize=' + pageSize + '&pageNumber=' + pageNumber;
    if (searchString !== '' || searchColumn !== '') uri += '&searchString=' + searchString;
    if (searchColumn !== '') uri += '&searchColumn=' + searchColumn;
    if (sortOrder !== '') uri += '&sortOrder=' + sortOrder;
    return this.http.get(`${this._url}/api/contacts?` + uri);
  }
}
