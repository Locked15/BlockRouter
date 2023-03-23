/**
 * This file contains some constants to work with.
 * ! This is public access file. !
 */
export default {
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
   * This property contains name of local storage value with access token.
   * @type {string}
   */
  tokenKeyName: 'access-token',

  /**
   * Contains name of local storage value with current user credentials.
   */
  currentUserKeyName: 'current-user',

  /**
   * This property contains routes to public-access pages.
   * @type {Array<string>}
   */
  publicPages: [
    '/login',
    '/:pathMatch(.*)*',
  ],
};
