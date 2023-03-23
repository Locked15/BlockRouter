export default class User {
  public userName: string;

  public password: string;

  public constructor(userName: string, password: string) {
    this.userName = userName;
    this.password = password;
  }

  public static getDefaultUser(): User {
    const user = new User('', '');
    return user;
  }
}
