import Brand from './entities/Brand';
import Model from './entities/Model';

export default class DashBoardModel {
  public brands: Array<Brand>;

  public models: Array<Model>;

  public constructor(brands: Array<Brand>, models: Array<Model>) {
    this.brands = brands;
    this.models = models;
  }

  public static getDefaultModel(): DashBoardModel {
    const defaultModel = new DashBoardModel(new Array<Brand>(), new Array<Model>());

    return defaultModel;
  }
}
