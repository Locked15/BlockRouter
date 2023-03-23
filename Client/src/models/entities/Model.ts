export default class Model {
  id: number;

  brandId: number;

  name: string;

  isActive: boolean;

  public constructor(id: number, brandId: number, name: string, isActive: boolean) {
    this.id = id;
    this.brandId = brandId;
    this.name = name;
    this.isActive = isActive;
  }
}
