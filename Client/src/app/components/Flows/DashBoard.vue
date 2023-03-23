<template>
  <div>
    <h2>Grats, you are USER!</h2>
  </div>
</template>

<script lang="ts">
  import DashBoardModel from '@/models/DashBoardModel';
  import Brand from '@/models/entities/Brand';
  import Model from '@/models/entities/Model';
  import axios, { AxiosRequestConfig } from 'axios';
  import Swal from 'sweetalert2';
  import { Vue } from 'vue-class-component';

  import appData from '../../data';

  export default class DashBoard extends Vue {
    public componentData = DashBoardModel.getDefaultModel();

    public data(): DashBoardModel {
      return this.componentData;
    }

    public async beforeMount() {
      this.componentData = await DashBoard.getDataFromApi();
      if (!this.componentData.brands.length && !this.componentData.models.length) {
        Swal.fire(
          'Just Nothingness',
          'Seems like API returned empty data-set.\nMaybe you could re-login or something?',
          'warning',
        );
      } else {
        Swal.fire('Data is Available', 'API-Query is completed successfully.\nEnjoy.');
      }
    }

    private static async getDataFromApi(): Promise<DashBoardModel> {
      const brands = await DashBoard.getBrandsFromApi();
      const models = await DashBoard.getModelsFromApi();

      return new DashBoardModel(brands, models);
    }

    private static async getBrandsFromApi(): Promise<Array<Brand>> {
      const result: Promise<Array<Brand>> = await axios
        .get(`${appData.apiAddress}${appData.getBrandsQuery}`, DashBoard.getJwtTokenConfig())
        .then((response) => response.data)
        .catch((error) => {
          if (error.response === undefined || error.response.status === undefined) {
            Swal.fire('Whoops, API is NA!', 'Looks like API is not available for now.\n\nPlease, stand by.', 'error');
          } else {
            Swal.fire('Something Went Wrong', error.response.data, 'error');
          }
        });

      // Return empty array, if request returns fail.
      return result || new Array<Brand>();
    }

    private static async getModelsFromApi(): Promise<Array<Model>> {
      const result: Promise<Array<Model>> = await axios
        .get(`${appData.apiAddress}${appData.getModelsQuery}`, DashBoard.getJwtTokenConfig())
        .then((response) => response.data)
        .catch((error) => {
          if (error.response === undefined || error.response.status === undefined) {
            Swal.fire('Whoops, API is NA!', 'Looks like API is not available for now.\n\nPlease, stand by.', 'error');
          } else {
            Swal.fire('Something Went Wrong', error.response.data, 'error');
          }
        });

      // Return empty array, if request returns fail.
      return result || new Array<Model>();
    }

    private static getJwtTokenConfig(): AxiosRequestConfig {
      const token = JSON.parse(localStorage.getItem(appData.tokenKeyName) || '{ }');
      const config = {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      };

      return config;
    }
  }
</script>

<style></style>
