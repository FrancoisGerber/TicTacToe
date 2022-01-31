export interface IBaseService {
    GetAll(): any;
    Get(id: string): any;
    Post(entity: any): any;
    Put(id: string, entity: any): any;
    Delete(id: string): any;
}
