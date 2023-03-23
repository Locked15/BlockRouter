/**
 * This file contains some constants to work with.
 * ! This is public access file. !
 */
export default {
  // #region API Addresses

  /**
   * Contains address of API (with port number) to make correct communication.
   * ! This one should be updated, when configuration changes. !
   * @type {string}
   */
  apiAddress: 'https://localhost:5001/api',

  /**
   * Contains api query address to sing user in.
   */
  signInApiQuery: '/user/signin/',

  /**
   * Contains API query address to get all brands.
   */
  getBrandsQuery: '/brand/read',

  /**
   * Contains API query address to get all models.
   */
  getModelsQuery: '/models/read',
  // #endregion API Addresses.

  // #region Key Names

  /**
   * This property contains name of local storage value with access token.
   * @type {string}
   */
  tokenKeyName: 'access-token',

  /**
   * Contains name of local storage value with current user credentials.
   */
  currentUserKeyName: 'current-user',
  // #endregion Key Names.

  /**
   * This property contains routes to public-access pages.
   * @type {Array<string>}
   */
  publicPages: [
    '/login',
    '/:pathMatch(.*)*',
  ],
};
