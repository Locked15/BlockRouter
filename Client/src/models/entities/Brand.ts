/**
 * Reproduction of entity 'Brand'.
 */
export default class Brand {
  public id: number;

  public name: string;

  public isActive: boolean;

  public constructor(id: number, name: string, isActive: boolean) {
    this.id = id;
    this.name = name;
    this.isActive = isActive;
  }
}
