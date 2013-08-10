using GalaSoft.MvvmLight;

namespace ExemploWinRT_InAppPurchase.ViewModel
{
    /// <summary>
    /// ViewModel contendo as propriedades e comandos a serem bindados na Main View.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Atributos e Propriedades
        /// <summary>
        /// Propriedade que expõe a instância Singleton de LicenseHelper.
        /// </summary>
        public LicenseHelper LicenseHelper
        {
            get
            {
                return ExemploWinRT_InAppPurchase.LicenseHelper.Current;
            }
        }
        /// <summary>
        /// Atalho para a propriedade AnuncioRemovido da classe LicenseHelper. Adicionada na ViewModel para suportar change notification quando
        /// o usuário compra a licença de "ANUNCIO_REMOVIDO".
        /// </summary>
        public bool AnuncioRemovido
        {
            get
            {
                return LicenseHelper.AnuncioRemovido;
            }
        }
        #endregion

        #region Construtores
        /// <summary>
        /// Constroi uma instância de MainViewModel, inicializando os comandos necessários.
        /// </summary>
        public MainViewModel()
        {
            RemoverAnuncio = new GalaSoft.MvvmLight.Command.RelayCommand(RemoverAnuncioAction, CanExecuteRemoverAnuncio);
        }
        #endregion

        #region Comandos
        #region Remover Anúncio
        /// <summary>
        /// Comando que pode ser utilizado para realizar a compra da licença de remoção do anúncio.
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand RemoverAnuncio { get; private set; }
        /// <summary>
        /// Ação que é executada quando o comando RemoverAnuncio é disparado.
        /// </summary>
        private async void RemoverAnuncioAction()
        {
            await LicenseHelper.RemoverAnuncioAsync().ContinueWith(
                (task) => GalaSoft.MvvmLight.Threading.DispatcherHelper.RunAsync(
                    () =>
                    {
                        RaisePropertyChanged("AnuncioRemovido");
                        RemoverAnuncio.RaiseCanExecuteChanged();
                    }));
        }
        /// <summary>
        /// Indica se o comando RemoverAnuncio pode ser executado ou não.
        /// Ele pode ser executado quando a licença da remoção do anúncio ainda não tiver sido comprada.
        /// </summary>
        /// <returns>True se o comando RemoverAnuncio pode ser executado. Senão retorna false.</returns>
        private bool CanExecuteRemoverAnuncio()
        {
            return !LicenseHelper.AnuncioRemovido;
        }
        #endregion
        #endregion
    }
}